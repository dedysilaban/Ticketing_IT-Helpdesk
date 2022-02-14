using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ReviewTicketVM
    {
        public int CaseId { get; set; }
        public int Review { get; set; }
        public string EmployeeId { get; set; }
    }
}
