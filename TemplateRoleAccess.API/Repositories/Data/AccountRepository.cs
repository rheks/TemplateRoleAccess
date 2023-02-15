using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Models.ViewModels;
using TemplateRoleAccess.API.Repositories.General;

namespace TemplateRoleAccess.API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public AccountRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<int> UpdatePassword(Account account)
        {
            var acts = await _appDbContext.Accounts.SingleOrDefaultAsync(a => a.NIK == account.NIK);
            acts.Password = BC.HashPassword(account.Password);

            _appDbContext.Update(acts);
            var response = await _appDbContext.SaveChangesAsync();

            return response;
        }

        public async Task<UserLoginVM> Login(LoginVM userLogin)
        {
            //var response = await _appDbContext.AccountRoles.SingleOrDefaultAsync(e => e.AccountNIK == userLogin.NIK);
            var response = await _appDbContext.AccountRoles.Where(ar => ar.Accounts.Employee.Email == userLogin.Email).SingleOrDefaultAsync();
            if (response == null || !BC.Verify(userLogin.Password, response.Accounts.Password))
            {
                return null;
            }

            // Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // Claims
            var claims = new[]
            {
                new Claim("NIK", response.Accounts.Employee.NIK),
                new Claim(ClaimTypes.Email, response.Accounts.Employee.Email),
                new Claim(ClaimTypes.Name, response.Accounts.Employee.FirstName + " " + response.Accounts.Employee.LastName),
                new Claim(ClaimTypes.Role, response.Roles.Name),
            };

            // Payload
            var TokenExpired = DateTime.Now.AddMinutes(10);
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                TokenExpired
            );

            var dataToken = new JwtSecurityToken(header, payload);
            var Token = new JwtSecurityTokenHandler().WriteToken(dataToken);

            return new UserLoginVM
            {
                NIK = response.Accounts.Employee.NIK,
                Name = response.Accounts.Employee.FirstName + " " + response.Accounts.Employee.LastName,
                Email = response.Accounts.Employee.Email,
                Role = response.Roles.Name,
                Token = Token,
                TokenExpires = TokenExpired
            };
        }
    }
}
