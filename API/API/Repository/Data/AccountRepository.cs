using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            var email = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
            if (email != null)
            {
                var account = myContext.Accounts.Where(a => a.NIK == email.NIK).FirstOrDefault();
                if (BCrypt.Net.BCrypt.Verify(loginVM.Password,account.Password))
                {
                    loginVM.Log = DateTime.Now;
                    return 1;
                }
                return 6;
            }
            return 0;
        }

        public int RequestOTP(ForgetPasswordVM forgetPasswordVM) {
            var data = myContext.Employees.Where(e => e.Email == forgetPasswordVM.Email).FirstOrDefault();
            if (data != null)
            {
                var account = myContext.Accounts.Find(data.NIK);
                Random r = new Random();
                account.OTP = r.Next(100000, 999999);
                account.IsUsed = false;
                account.ExpiredToken = DateTime.Now.AddMinutes(5);
                myContext.Entry(account).State = EntityState.Modified;
                myContext.SaveChanges();
                return SendMail(account, data); ;
            }
            return 0;
        }
        
        public int ChangePassword(ForgetPasswordVM forgetPasswordVM) {
            var data = myContext.Employees.Where(e => e.Email == forgetPasswordVM.Email).FirstOrDefault();
            if (data != null)
            {
                var account = myContext.Accounts.Find(data.NIK);
                if (account.OTP == forgetPasswordVM.OTP)
                {
                    if (account.ExpiredToken >= DateTime.Now)
                    {
                        if (account.IsUsed == false)
                        {
                            account.Password = BCrypt.Net.BCrypt.HashPassword(forgetPasswordVM.Password);
                            account.IsUsed = true;
                            myContext.Entry(account).State = EntityState.Modified;
                            return myContext.SaveChanges();
                        }
                        return 8;
                    }
                    return 6;
                }
                return 7;
            }
            return 0;
        }

        public int SendMail(Account account, Employee employee) {
            string to = employee.Email;
            string from = "mccreg61net@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "OTP";
            message.Body = $"Your OTP : {account.OTP} \nExpired Token : {account.ExpiredToken}";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, "61mccregnet"),
                EnableSsl = true
            };
            try
            {
                client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Object> GetRoles(string mail) {
            var cek = myContext.Employees.Where(e => e.Email == mail).FirstOrDefault();
            var role = myContext.AccountRoles.Where(a => a.NIK == cek.NIK).Select(ar => ar.Role.Name).ToList();
            return role;
        }
    }
}
