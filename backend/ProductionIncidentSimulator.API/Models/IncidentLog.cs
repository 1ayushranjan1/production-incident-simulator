using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionIncidentSimulator.API.Models
{
    public class IncidentLog
    {
        [Key]
        [Column("incidentid")]
        public int IncidentId { get; set; }

        [Column("orderid")]
        public int? OrderId { get; set; }

        [Column("errormessage")]
        public string ErrorMessage { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
        
        [Column("retrycount")]
        public int RetryCount { get; set; }

    }
}
