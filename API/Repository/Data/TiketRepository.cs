using API.Context;
using API.Models;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class TicketRepository : GeneralRepository<MyContext, Ticket, int>
    {
        private readonly MyContext context;

        public TicketRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        SmtpClient client = new SmtpClient();
        ServiceEmail serviceEmail = new ServiceEmail();
        public int CreateTicket(TicketVM ticketVM)
        {
            int result = 0;
            var message = "You success create ticket, your ticket being process as soon as possible, please wait!";
            var user = context.Employee.Find(ticketVM.EmployeeId);
            if(user == null)
            {
                return 0;
            }
            serviceEmail.SendEmail(user.Email, message);
           
            {
                Ticket tickets = new Ticket()
                {
                    Description = ticketVM.Description,
                    StartDateTime = DateTime.Now,
                    Review = 0,
                    EmployeeId = ticketVM.EmployeeId,
                    TechnicalId = "",
                    CategoryId = ticketVM.CategoryId
                };
                context.Add(tickets);
                result = context.SaveChanges();

                Convertation convertation = new Convertation()
                {
                    DateTime = DateTime.Now,
                    Message = ticketVM.Message,
                    TicketId = tickets.Id,
                    EmployeeId = ticketVM.EmployeeId
                };
                context.Add(convertation);
                result = context.SaveChanges();

                History history = new History()
                {
                    TicketId = tickets.Id,
                    Description = "[SYSTEM] Client create Ticket and Ask Admin",
                    DateTime = DateTime.Now,
                    Level = 1,
                    EmployeeId = ticketVM.EmployeeId,
                    StatusCodeId = 1
                };
                context.Add(history);
                result = context.SaveChanges();
            }
            return result;
        }
        public IEnumerable<CaseVM> GetCase()
        {
            Ticket cases = new Ticket();
            var all = (
                from c in context.Cases
                join u in context.Employee on c.EmployeeId equals u.Id
                join ct in context.Categories on c.CategoryId equals ct.Id 
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CategoryName = ct.Name
                }).ToList();
            return all;
        }

        public IEnumerable<CaseVM> ViewTicketsByUserId(string employeeId)
        {
            Ticket cases = new Ticket();
            var all = (
                from c in context.Cases
                join u in context.Employee on c.EmployeeId equals u.Id
                join ct in context.Categories on c.CategoryId equals ct.Id
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CategoryName = ct.Name
                }).ToList();
            return all.Where(x => x.EmployeeId == employeeId);
        }

       
        public IEnumerable<Ticket> GetCases()
        {
            var all = context.Cases.Where(x => x.EndDateTime == null).OrderByDescending(x => x.StartDateTime);
            return all;
        }

        public IEnumerable<CaseVM> ViewTicketsByStaffId(int userId)
        {
            Ticket cases = new Ticket();
            var all = (
                from c in context.Cases
                join u in context.Employee on c.EmployeeId equals u.Id
               
                join ct in context.Categories on c.CategoryId equals ct.Id
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CategoryName = ct.Name
                }).ToList();
            return all; 
        }

        public IEnumerable<CaseVM> ViewTicketsByLevel(int level)
        {
            Ticket cases = new Ticket();
            var all = (
                from c in context.Cases
                join u in context.Employee on c.EmployeeId equals u.Id
              
                join ct in context.Categories on c.CategoryId equals ct.Id
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    EmployeeId = u.Id,
                    TechnicalId = c.TechnicalId,
                    UserName = u.Name,
                    CategoryName = ct.Name
                }).ToList();
            return all.Where(x => x.EndDateTime == null && x.Level == level && (x.TechnicalId == null || x.TechnicalId == "")).OrderByDescending(x => x.StartDateTime);
        }

        public int AskNextLevel(int caseId)
        {
            int result = 0;

            var get = context.Histories.OrderByDescending(e => e.DateTime).FirstOrDefault(x => x.TicketId == caseId);
            if(get == null)
            {
                return 0;
            }
            var cases = context.Cases.Find(caseId);
            if(cases == null)
            {
                return 0;
            }

            if (get.Level < 3) {
                cases.TechnicalId = "";
                context.Cases.Update(cases);
                result = context.SaveChanges();

                var history = new History()
                {
                    DateTime = DateTime.Now,
                    Level = get.Level + 1,
                    Description = $"[STAFF] UserId #{get.EmployeeId} Ask Help CaseId #{caseId} to Level #{get.Level + 1}",
                    EmployeeId = get.EmployeeId,
                    TicketId = get.TicketId,
                    StatusCodeId = 2
                };

                context.Histories.Add(history);
                result = context.SaveChanges();
            }
            return result;
        }

        

        public int HandleTicket(CloseTicketVM closeTicketVM)
        {
            int result = 0;

            var get = context.Histories.OrderByDescending(e => e.DateTime).FirstOrDefault(x => x.TicketId == closeTicketVM.CaseId);
            if (get.Level < 3)
            {
                var history = new History()
                {
                    DateTime = DateTime.Now,
                    Description = $"[STAFF] TechnicalId #{closeTicketVM.EmployeeId} Handling CaseId #{closeTicketVM.CaseId}",
                    EmployeeId = closeTicketVM.EmployeeId,
                    Level = get.Level,
                    TicketId = get.TicketId,
                    StatusCodeId = 2
                };

                var getCase = context.Cases.Find(closeTicketVM.CaseId);
                getCase.TechnicalId= closeTicketVM.EmployeeId;

                context.Cases.Update(getCase);
                result = context.SaveChanges();

                context.Histories.Add(history);
                result = context.SaveChanges();
            }
            return result;
        }

        public IEnumerable<CaseVM> ViewHistoryTicketsByUserId(string employeeId)
        {
            Ticket cases = new Ticket();
            var all = (
                from c in context.Cases
                join u in context.Employee on c.EmployeeId equals u.Id
                join ct in context.Categories on c.CategoryId equals ct.Id
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CategoryName = ct.Name,
                }).ToList();
            return all.Where(x => x.EmployeeId == employeeId && (x.Review != null || x.Review > 0));
        }

        public IEnumerable<CaseVM> ViewHistoryTicketsByStaffId(int userId)
        {

            Ticket cases = new Ticket();
            var all = (
                from c in context.Cases
                join u in context.Employee on c.EmployeeId equals u.Id

                join ct in context.Categories on c.CategoryId equals ct.Id
                select new CaseVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDateTime = c.StartDateTime,
                    EndDateTime = c.EndDateTime,
                    Review = c.Review,
                    EmployeeId = u.Id,
                    UserName = u.Name,
                    CategoryName = ct.Name
                }).ToList();
            return all;
        }

        public int CloseTicketById(CloseTicketVM closeTicketVM)
        {
            int result = 0;
            var cases = context.Cases.Find(closeTicketVM.CaseId);
            if (cases.EndDateTime == null)
            {
                cases.EndDateTime = DateTime.Now;
                context.Cases.Update(cases);
                context.SaveChanges();

                var lastHistory = context.Histories.OrderByDescending(e => e.DateTime).FirstOrDefault(x => x.TicketId == closeTicketVM.CaseId);
                History newHistory = new History()
                {
                    TicketId = cases.Id,
                    Description = $"[SYSTEM] Closed Ticket By Staff #{closeTicketVM.EmployeeId}",
                    DateTime = DateTime.Now,
                    Level = lastHistory.Level,
                    EmployeeId = closeTicketVM.EmployeeId,
                    StatusCodeId = 3
                };

                var getCase = context.Cases.Find(closeTicketVM.CaseId);
                getCase.TechnicalId = closeTicketVM.EmployeeId;

                context.Cases.Update(getCase);
                result = context.SaveChanges();

                context.Histories.Add(newHistory);
                result = context.SaveChanges();
                return result;
            }
            else
            {
                return 0;
            }
        }
        
    }
}
























/*

 public int ReviewTicket(ReviewTicketVM reviewTicketVM)
        {
            int result = 0;
            var cases = context.Cases.Find(reviewTicketVM.CaseId);
            if(cases == null)
            {
                return result;
            }
            if (cases.EndDateTime != null)
            {
                cases.Review = reviewTicketVM.Review;
                context.Cases.Update(cases);
                result = context.SaveChanges();

                var lastHistory = context.Histories.OrderByDescending(e => e.DateTime).FirstOrDefault();
                History newHistory = new History()
                {
                    TicketId = cases.Id,
                    Description = $"[CLIENT] Ticket #{cases.Id} Reviewed by User #{reviewTicketVM.EmployeeId}",
                    DateTime = DateTime.Now,
                    Level = lastHistory.Level,
                    EmployeeId = reviewTicketVM.EmployeeId,
                    StatusCodeId = lastHistory.StatusCodeId
                };
                context.Histories.Add(newHistory);
                result = context.SaveChanges();
                return result;
            }
            else
            {
                return 0;
            }
        }


*/
