using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class CreateConvertationVM
    {
        public string EmployeeId { get; set; }
        public int CaseId { get; set; }
        public string Message { get; set; }
    }
}
