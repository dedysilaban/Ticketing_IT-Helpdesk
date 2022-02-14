using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class HistoryVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int Level { get; set; }
        public string EmployeeId { get; set; }
        public string UserName { get; set; }
        public string CaseName { get; set; }
        public string StatusCodeName { get; set; }
    }
}
