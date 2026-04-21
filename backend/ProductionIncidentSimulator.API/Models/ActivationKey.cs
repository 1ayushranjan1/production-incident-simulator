using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionIncidentSimulator.API.Models
{
    public class ActivationKey
    {
        [Key]
        [Column("keyid")]   //
        public int KeyId { get; set; }

        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("productkey")]
        public string ProductKey { get; set; }

        [Column("isdelivered")]   //
        public bool IsDelivered { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }

        public Order Order { get; set; }
    }
}
