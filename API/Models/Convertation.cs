using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_Convertation")]
    public class Convertation
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public int TicketId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
