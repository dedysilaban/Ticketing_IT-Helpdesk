using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ConvertationVM
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string CaseName { get; set; }
        public int CaseId { get; set; }
        public string EmployeeId{ get; set; }
        public string UserName { get; set; }
    }
}
