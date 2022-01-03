using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        [HttpPost("{Login}")]
        public ActionResult Login(LoginVM loginVM)
        {
            var response = accountRepository.Login(loginVM);
            switch (response)
            {
                case 0:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Email Belum Terdaftar" });
                case 1:
                    var getRole = accountRepository.GetRoles(loginVM.Email);
                    var claims = new List<Claim>
                    {
                        new Claim("Email", loginVM.Email)
                    };
                    foreach (var role in getRole)
                    {
                        claims.Add(new Claim("roles", role.ToString()));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                        );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idToken.ToString()));
                    return Ok(new { status = HttpStatusCode.OK, idToken, message = "Berhasil Login" });
                case 6:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Password Salah" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Login Gagal" });
            }
        }

        [HttpPut("RequestOTP")]
        public ActionResult RequestOTP(ForgetPasswordVM forgetPasswordVM)
        {
            var response = accountRepository.RequestOTP(forgetPasswordVM);
            switch (response)
            {
                case 0:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Email Belum Terdaftar \nSilahkan Melakukan Registrasi" });
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Berhasil Mengirim Request OTP ke Email \nSilahkan Lihat Email Anda Baik Dalam Spam atapun Inbox" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Mengirim Request" });
            }
        }

        [HttpPut("{ChangePassword}")]
        public ActionResult ChangePassword(ForgetPasswordVM forgetPasswordVM)
        {
            var response = accountRepository.ChangePassword(forgetPasswordVM);
            switch (response)
            {
                case 0:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Email Belum Terdaftar \nSilahkan Melakukan Registrasi" });
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Berhasil Mengganti Password" });
                case 6:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Token Sudah Expired \nSilahkan Melakukan Request OTP" });
                case 7:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "OTP Anda Salah \nSilahkan Cek Email Anda atau Melakukan Request Kembali" });
                case 8:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "OTP Sudah di Gunakan \nSilahkan Melakukan Request OTP" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Mengganti Password" });
            }
        }

    }
}
