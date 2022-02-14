using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ConvertationRepository : GeneralRepository<MyContext, Convertation, int>
    {
        private readonly MyContext context;
        public ConvertationRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int CreateConvertation(CreateConvertationVM createConvertationVM)
        {
            var convertation = new Convertation()
            {
                DateTime = DateTime.Now,
                Message = createConvertationVM.Message,
                EmployeeId = createConvertationVM.EmployeeId,
                TicketId = createConvertationVM.CaseId
            };
            context.Convertations.Add(convertation);
            context.SaveChanges();
            return convertation.Id;
        }

        public IEnumerable<ConvertationVM> GetConvertation()
        {
            Convertation convertation = new Convertation();
            var all = (
                from cv in context.Convertations
                join u in context.Employee on cv.EmployeeId equals u.Id
                join c in context.Cases on cv.TicketId equals c.Id
                select new ConvertationVM
                {
                    Id = cv.Id,
                    DateTime = cv.DateTime,
                    Message = cv.Message,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CaseName = c.Description
                }).ToList();
            return all.OrderByDescending(x => x.DateTime); 
        }

        public IEnumerable<Convertation> ViewConvertations()
        {
            var view = context.Convertations.ToList();
            return view;
        }

        public IEnumerable<ConvertationVM> ViewConvertationsByCaseId(int id)
        {
            Convertation convertation = new Convertation();
            var all = (
                from cv in context.Convertations
                join u in context.Employee on cv.EmployeeId equals u.Id
                join c in context.Cases on cv.TicketId equals c.Id
                select new ConvertationVM
                {
                    Id = cv.Id,
                    DateTime = cv.DateTime,
                    Message = cv.Message,
                    CaseId = c.Id,
                    EmployeeId = u.Id,
                    UserName = u.Name
                }).ToList();
            return all.Where(x => x.CaseId == id);
        }

        public IEnumerable<Convertation> ViewConvertationsByUserId(string employeeId)
        {
            var find = context.Convertations.Where(x => x.EmployeeId == employeeId);
            return find;
        }
    }
}
