using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_ticket")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? Review { get; set; }
        public string EmployeeId { get; set; }
        public string TechnicalId { get; set; }

        public int CategoryId { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<Convertation> Convertation { get; set; }
        public virtual Category Category { get; set; }
    }
}
