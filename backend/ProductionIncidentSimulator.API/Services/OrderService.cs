using ProductionIncidentSimulator.API.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProductionIncidentSimulator.API.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(CreateOrderRequest request)
        {
            var order = new Order
            {
                CustomerName = request.CustomerName,
                Vendor = request.Vendor,
                Status = "Pending",
                CreatedDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }
        public Order CreateOrderWithKeys(CreateOrderRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var order = new Order
                {
                    CustomerName = request.CustomerName,
                    Vendor = request.Vendor,
                    Status = "Pending",
                    CreatedDate = DateTime.UtcNow
                };

                _context.Orders.Add(order);
                _context.SaveChanges(); //  OrderId generated

                var keys = new List<ActivationKey>
        {
            new ActivationKey
            {
                OrderId = order.OrderId,
                ProductKey = "KEY-123",
                IsDelivered = false,
                CreatedDate = DateTime.UtcNow
            },
            new ActivationKey
            {
                OrderId = order.OrderId,
                ProductKey = "KEY-456",
                IsDelivered = false,
                CreatedDate = DateTime.UtcNow
            }
        };

                _context.ActivationKeys.AddRange(keys);
                _context.SaveChanges();
                if (request.CustomerName == "FAIL")
                {
                    throw new Exception("Simulated production failure");
                }

                transaction.Commit();  // success

                return order;
            }
            catch (Exception ex)
            {
                transaction.Rollback();  //  everything undone
                var incident = new IncidentLog
                {
                    OrderId = null, // or order?.OrderId if available
                    ErrorMessage = ex.Message,
                    Status = "Failed",
                    CreatedDate = DateTime.UtcNow
                };

                _context.IncidentLogs.Add(incident);
                _context.SaveChanges();

                throw;
            }
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public void RetryIncident(int incidentId)
        {
            var incident = _context.IncidentLogs
                .FirstOrDefault(x => x.IncidentId == incidentId);

            if (incident == null)
                throw new Exception("Incident not found");

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var order = new Order
                {
                    CustomerName = "RetryUser",
                    Vendor = "RetryVendor",
                    Status = "Pending",
                    CreatedDate = DateTime.UtcNow
                };

                var key = new ActivationKey
                {
                    ProductKey = "RETRY-KEY",
                    IsDelivered = false,
                    CreatedDate = DateTime.UtcNow
                };

                order.ActivationKeys = new List<ActivationKey> { key };

                _context.Orders.Add(order);

                incident.Status = "Resolved";
                incident.RetryCount += 1;

                _context.SaveChanges(); //  single save

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                incident.Status = "RetryFailed";
                incident.ErrorMessage = ex.Message;
                incident.RetryCount += 1;

                _context.SaveChanges(); //  save failure state
            }
        }

    }
}
