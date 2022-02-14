using API.Context;
using API.Models;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        private readonly DbSet<RegisterVM> entities;
        public IConfiguration Configuration;
        public EmployeeRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            entities = context.Set<RegisterVM>();
            Configuration = configuration;
        }
        SmtpClient client = new SmtpClient();
        ServiceEmail serviceEmail = new ServiceEmail();
       
        public int Register(RegisterVM registerVM)
        {
            var message = "Your register in IT Support Helpdesk is Successfull!!";
            serviceEmail.SendEmail(registerVM.Email, message);
            var result = 0;
            var cek = context.Employee.FirstOrDefault(u => u.Email == registerVM.Email);
            if (cek == null)
            {
                Employee user = new Employee()
                {
                    Id = registerVM.EmployeeId,
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password),
                    BirthDate = registerVM.BirthDate,
                    RoleId = registerVM.RoleId,
                    Phone = registerVM.Phone,
                    Address = registerVM.Address,
                    Department = registerVM.Department
                };
                context.Add(user);
                result = context.SaveChanges();
            }
            return result;
        }

        public int CreateUser(Employee user)
        {
            var cek = context.Employee.FirstOrDefault(u => u.Email == user.Email);
            if(cek == null)
            {
                context.Add(user);
                return context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public string GenerateTokenLogin(LoginVM loginVM)
        {
            var user = context.Employee.FirstOrDefault(p => p.Email == loginVM.Email);
            var ar = context.Roles.Single(ar => ar.Id == user.RoleId);
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("role",ar.Name)
                    //new Claim(ClaimTypes.Role,ar.Role.RoleName)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public int Login(LoginVM loginVM)
        {
            var cek = context.Employee.FirstOrDefault(u => u.Email == loginVM.Email);
            if (cek == null)
            {
                return 404;
            }

            if (BCrypt.Net.BCrypt.Verify(loginVM.Password, cek.Password))
            {
                return 1;
            }
            else
            {
                return 401;
            }
        }

        // Clients
        public IEnumerable<ProfileVM> GetClients()
        {
            Employee user = new Employee();
            var all = (
                from u in context.Employee
                join r in context.Roles on u.RoleId equals r.Id
                select new ProfileVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    RoleName = r.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Department = u.Department,
                    Detail = u.Detail
                }).ToList();
            return all.Where(x => x.RoleName == "Client");
        }

        public ProfileVM GetClientById(string id)
        {
            var all = (
                from u in context.Employee
                join r in context.Roles on u.RoleId equals r.Id
                select new ProfileVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    RoleName = r.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Department = u.Department,
                    Detail = u.Detail
                }).ToList();
            return all.FirstOrDefault(u => u.Id == id);
        }
        public int DeleteClientById(string id)
        {
            var employee = context.Employee.Find(id);
            if (employee != null)
            {
                context.Remove(employee);
                context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // Staffs
        public IEnumerable<ProfileVM> GetStaffs()
        {
            Employee user = new Employee();
            var all = (
                from u in context.Employee
                join r in context.Roles on u.RoleId equals r.Id
                select new ProfileVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    RoleName = r.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Department = u.Department,
                    Detail = u.Detail
                }).ToList();
            return all.Where(x => x.RoleName != "Client");
        }
        public ProfileVM GetStaffById(string id)
        {
            var all = (
               from u in context.Employee
               join r in context.Roles on u.RoleId equals r.Id
               select new ProfileVM
               {
                   Id = u.Id,
                   Name = u.Name,
                   Email = u.Email,
                   BirthDate = u.BirthDate,
                   RoleName = r.Name,
                   Phone = u.Phone,
                   Address = u.Address,
                   Department = u.Department,
                   Detail = u.Detail
               }).ToList();
            return all.FirstOrDefault(u => u.Id == id);
        }

        public UserSessionVM GetUserByEmail(string Email)
        {
            var all = (
               from u in context.Employee
               join r in context.Roles on u.RoleId equals r.Id
               select new UserSessionVM
               {
                   EmployeeId = u.Id,
                   Name = u.Name,
                   Email = u.Email,
                   Role = r.Name,
                   RoleId = r.Id
               }).ToList();
            return all.FirstOrDefault(u => u.Email == Email);
        }

        public int DeleteStaffById(string id)
        {
            var user = context.Employee.Find(id);
            if (user != null)
            {
                context.Remove(user);
                context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //Profile
        public IEnumerable<ProfileVM> GetProfile()
        {
            Employee user = new Employee();
            var all = (
                from u in context.Employee
                join r in context.Roles on u.RoleId equals r.Id
                select new ProfileVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    RoleName = r.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Department = u.Department,
                    Detail = u.Detail
                }).ToList();
            return all;
        }
        // Users
        public int UpdateProfile(Employee updateEmployee)
        {
            var result = 0;
            var employee = context.Employee.FirstOrDefault(u => u.Email == updateEmployee.Email);
            if (employee != null)

            {
                employee.Id = updateEmployee.Id;
                employee.Name = updateEmployee.Name;
                employee.BirthDate = updateEmployee.BirthDate;
                employee.Phone = updateEmployee.Phone;
                employee.Address = updateEmployee.Address;
                employee.Department = updateEmployee.Department;

                if (updateEmployee.Password != "")
                {
                    employee.Password = BCrypt.Net.BCrypt.HashPassword(updateEmployee.Password);
                }
                context.Employee.Update(employee);
                result = context.SaveChanges();
            }
            return result;
        }
        public List<Employee> GetUsers()
        {
            return new List<Employee>();
        }
        public Employee GetUserById(int id)
        {
            return new Employee();
        }
        public int UpdateUser(Employee user)
        {
            return 0;
        }
        public int DeleteUserById(int id)
        {
            return 0;
        }
    }

}
