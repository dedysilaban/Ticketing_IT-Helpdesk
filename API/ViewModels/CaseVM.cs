using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class CaseVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? Review { get; set; }
        public int Level { get; set; }
        public string EmployeeId { get; set; }
        public string TechnicalId { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        //public int StatusCodeId { get; set; }
        //public string StatusCodeName { get; set; }
    }
}
