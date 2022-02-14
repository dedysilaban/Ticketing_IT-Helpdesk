using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class HistoryRepository : GeneralRepository<MyContext, History, int>
    {
        private readonly MyContext context;
        public HistoryRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public IEnumerable<HistoryVM> GetHistory()
        {
            History history = new History();
            var all = (
                from h in context.Histories
                join u in context.Employee on h.EmployeeId equals u.Id
                join c in context.Cases on h.TicketId equals c.Id
                join sc in context.StatusCodes on h.StatusCodeId equals sc.Id
                select new HistoryVM
                {
                    Id = h.Id,
                    Description = h.Description,
                    DateTime = h.DateTime,
                    Level = h.Level,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CaseName = c.Description,
                    StatusCodeName = sc.Name
                }).ToList();
            return all.OrderByDescending(x => x.DateTime);
        }
    }
}
