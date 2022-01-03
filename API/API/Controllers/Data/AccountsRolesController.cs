using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        private readonly AccountRoleRepository accountRoleRepository;

        public AccountsRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }
    }
}
