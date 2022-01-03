using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, string>
    {
        private readonly MyContext myContext;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int AssignManager(AccountRole accountRole)
        {
            var cek = myContext.Employees.Where(e => e.NIK == accountRole.NIK).FirstOrDefault();
            if (cek != null)
            {
                var manager = myContext.AccountRoles.ToList().Where(ar => ar.NIK == accountRole.NIK);
                foreach (var ar in manager)
                {
                    if (ar.RoleId == "RS002")
                    {
                        return 2;
                    }
                }
                int incrementAccountRole = myContext.AccountRoles.ToList().Count + 1;
                string idAccountRole = "AR00" + incrementAccountRole.ToString();
                var accRole = new AccountRole
                {
                    Id = idAccountRole,
                    NIK = cek.NIK,
                    RoleId = "RS002"
                };
                myContext.AccountRoles.Add(accRole);
                return myContext.SaveChanges();
            }
            return 0;
        }
    }
}
