using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class TicketVM
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime StartDateTime { get; set; }
        public int CategoryId { get; set; }
        public string Message { get; set; }
        public string EmployeeId { get; set; }
        public ICollection<IFormFile> Files { get; set; }

    }
}
