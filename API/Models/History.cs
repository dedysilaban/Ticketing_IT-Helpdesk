using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_history")]
    public class History
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int Level { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int StatusCodeId { get; set; }
        public virtual StatusCode StatusCode { get; set; }
    }
}
