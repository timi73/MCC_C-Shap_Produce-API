using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM) {

            int incrementEmployee = myContext.Employees.ToList().Count + 1;
            string formatedNIK = DateTime.Now.ToString("yyyy") + "0" + incrementEmployee.ToString();
            int incrementAccountRole = myContext.AccountRoles.ToList().Count + 1;
            string idAccountRole = "AR00" + incrementAccountRole.ToString();
            var email = myContext.Employees.Where(e => e.Email == registerVM.Email).FirstOrDefault();
            var phone = myContext.Employees.Where(e => e.Phone == registerVM.PhoneNumber).FirstOrDefault();

            if (email != null)
            {
                if (phone != null)
                {
                    return 5;
                }
                return 4;
            }

            var employee = new Employee {
                NIK = formatedNIK,
                FirstName = registerVM.FristName,
                LastName = registerVM.LastName,
                Phone = registerVM.PhoneNumber,
                Birthdate = registerVM.BirthDate,
                Slary = registerVM.Salary,
                Email = registerVM.Email
            };
            myContext.Employees.Add(employee);

            var account = new Account {
                NIK = employee.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
            };
            myContext.Accounts.Add(account);

            var education = new Education {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };
            myContext.Educations.Add(education);
            var accountRole = new AccountRole
            {
                Id = idAccountRole,
                NIK = employee.NIK,
                RoleId = "RS003"
            };
            myContext.AccountRoles.Add(accountRole);

            myContext.SaveChanges();

            var profiling = new Profiling
            {
                NIK = employee.NIK,
                EducationId = education.Id
            };

            myContext.Profilings.Add(profiling);
            return myContext.SaveChanges();
        }

        public IEnumerable<GetRegisterVM> GetRegister()
        {
            var query = (from em in myContext.Set<Employee>() join a in myContext.Set<Account>() on em.NIK equals a.NIK
                         join p in myContext.Set<Profiling>() on a.NIK equals p.NIK
                         join e in myContext.Set<Education>() on p.EducationId equals e.Id
                         join u in myContext.Set<University>() on e.UniversityId equals u.Id
                         select new GetRegisterVM() {
                             FullName = em.FirstName + " " + em.LastName,
                             Phone = em.Phone,
                             BirthDate = em.Birthdate,
                             Salary = em.Slary,
                             Email = em.Email,
                             Password = a.Password,
                             Degree = e.Degree,
                             GPA = e.GPA,
                             UniversityName = u.Name
                         }
                );
            return query.ToList();
        }

        public IEnumerable GetRegister2()
        {
            var result = myContext.Employees.Include(a => a.Account).ThenInclude(p => p.Profiling).ThenInclude(e => e.Education).ThenInclude(u => u.University);

            return result.ToList();
        }

        


    }
}
