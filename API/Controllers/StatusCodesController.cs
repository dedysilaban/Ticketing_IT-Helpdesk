using API.Base;
using API.Models;
using API.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class StatusCodesController : BaseController<StatusCode, StatusCodeRepository, int>
    {
        private readonly StatusCodeRepository statusCodeRepository;
        public StatusCodesController(StatusCodeRepository statusCodeRepository) : base(statusCodeRepository)
        {
            this.statusCodeRepository = statusCodeRepository;
        }
    }
}