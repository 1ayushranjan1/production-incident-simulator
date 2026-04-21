using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionIncidentSimulator.API.Models
{
    public class Order
    {
        [Key]
        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("customername")]
        public string CustomerName { get; set; }

        [Column("vendor")]
        public string Vendor { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }

        public List<ActivationKey> ActivationKeys { get; set; } = new List<ActivationKey>();
    }
}
