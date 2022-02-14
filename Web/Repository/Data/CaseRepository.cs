using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Base;

namespace Web.Repository.Data
{
    public class CaseRepository : GeneralRepository<Ticket, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public CaseRepository(Address address, string request = "Tickets/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<CaseVM>> GetCases()
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetTicketsByStaffId(string employeeId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewTicketsByStaffId/" + employeeId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetTicketsByUserId(string employeeId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewTicketsByUserId/" + employeeId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetHistoryTicketsByUserId(string employeeId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewHistoryTicketsByUserId/" + employeeId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetHistoryTicketsByStaffId(string employeeId)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewHistoryTicketsByStaffId/" + employeeId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }

        public async Task<List<CaseVM>> GetTicketsByLevel(int level)
        {
            List<CaseVM> data = new List<CaseVM>();

            using (var response = await httpClient.GetAsync(request + "ViewTicketsByLevel/" + level))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<CaseVM>>(apiResponse);
            }
            return data;
        }
    }
}
