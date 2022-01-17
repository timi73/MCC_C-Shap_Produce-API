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

            int incrementEmployee = myContext.Employees.ToList().Count;
            string formatedNIK = "";
            if (incrementEmployee == 0)
            {
                formatedNIK = DateTime.Now.Year + "0" + incrementEmployee.ToString();
            }
            else
            {
                string incrementEmployee2 = myContext.Employees.ToList().Max(e => e.NIK);
                int setNIk = Int32.Parse(incrementEmployee2) + 1;
                formatedNIK = setNIk.ToString();
            }
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
                Email = registerVM.Email,
                Gender = registerVM.Gender
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
                         select new GetRegisterVM
                         {
                             NIK = em.NIK,
                             FirstName = em.FirstName,
                             LastName = em.LastName,
                             Phone = em.Phone,
                             BirthDate = em.Birthdate,
                             Salary = em.Slary,
                             Email = em.Email,
                             Gender = em.Gender,
                             Password = a.Password,
                             Degree = e.Degree,
                             GPA = e.GPA,
                             EducationId = e.Id,
                             UniversityId = u.Id,
                             UniversityName = u.Name,
                             roleName = myContext.AccountRoles.Where(accountrole => accountrole.NIK == em.NIK).Select(accountrole => accountrole.Role.Name).ToList()
                         }
                );
            return query.ToList();
        }

        public GetRegisterVM GetRegisterId(string Nik)
        {

            //var query = (from em in myContext.Set<Employee>()
            //             join a in myContext.Set<Account>() on em.NIK equals a.NIK
            //             join p in myContext.Set<Profiling>() on a.NIK equals p.NIK
            //             join e in myContext.Set<Education>() on p.EducationId equals e.Id
            //             join u in myContext.Set<University>() on e.UniversityId equals u.Id
            //             where em.NIK == Nik
            //             select new GetRegisterVM
            //             {
            //                 NIK = em.NIK,
            //                 FirstName = em.FirstName,
            //                 LastName = em.LastName,
            //                 Phone = em.Phone,
            //                 BirthDate = em.Birthdate,
            //                 Salary = em.Slary,
            //                 Email = em.Email,
            //                 Gender = em.Gender,
            //                 Password = a.Password,
            //                 Degree = e.Degree,
            //                 GPA = e.GPA,
            //                 EducationId = e.Id,
            //                 UniversityId = u.Id,
            //                 UniversityName = u.Name,
            //                 roleName = myContext.AccountRoles.Where(accountrole => accountrole.NIK == em.NIK).Select(accountrole => accountrole.Role.Name).ToList()
            //             }
            //    );
            //return query;
            var query = myContext.Employees.Where(e => e.NIK == Nik).Include(a => a.Account).ThenInclude(p => p.Profiling).ThenInclude(e => e.Education).ThenInclude(u => u.University).FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            var selectData = new GetRegisterVM
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Phone = query.Phone,
                BirthDate = query.Birthdate,
                Salary = query.Slary,
                Email = query.Email,
                Gender = query.Gender,
                Password = query.Account.Password,
                Degree = query.Account.Profiling.Education.Degree,
                GPA = query.Account.Profiling.Education.GPA,
                EducationId = query.Account.Profiling.Education.Id,
                UniversityId = query.Account.Profiling.Education.UniversityId,
                UniversityName = query.Account.Profiling.Education.University.Name,
                roleName = myContext.AccountRoles.Where(accountrole => accountrole.NIK == query.NIK).Select(accountrole => accountrole.Role.Name).ToList()
            };

            return selectData;
        }

        public IEnumerable GetRegister2()
        {
            var result = myContext.Employees.Include(a => a.Account).ThenInclude(p => p.Profiling).ThenInclude(e => e.Education).ThenInclude(u => u.University);

            return result.ToList();
        }

        public int UpdateRegister(RegisterVM registerVM)
        {
            var nik = myContext.Employees.Where(e => e.NIK == registerVM.NIK).FirstOrDefault();
            var eid = myContext.Educations.Where(e => e.Id == registerVM.EducationId).FirstOrDefault();

            if (nik == null)
            {
                if (eid == null)
                {
                    return 5;
                }
                return 4;
            }
            myContext.Entry(nik).State = EntityState.Detached;
            myContext.Entry(eid).State = EntityState.Detached;

            var employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FristName,
                LastName = registerVM.LastName,
                Phone = registerVM.PhoneNumber,
                Birthdate = registerVM.BirthDate,
                Slary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = registerVM.Gender
            };
            myContext.Entry(employee).State = EntityState.Modified;
            myContext.SaveChanges();

            var education = new Education
            {
                Id = registerVM.EducationId,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };
            myContext.Entry(education).State = EntityState.Modified;
            return myContext.SaveChanges();
        }


    }
}
