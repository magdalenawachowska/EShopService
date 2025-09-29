using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Exceptions;
using User.Domain.Models;

namespace User.Application.Services
{
    public class LoginService : ILoginService
    {
        protected IJwtTokenService _jwtTokenService;

        public LoginService(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public string Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                var roles = new List<string> { "Client", "Employee", "Administrator" };
                var token = _jwtTokenService.GenerateToken(123, roles);
                // powinno być bardziej tak: 
                //var token = _jwtTokenService.GenerateToken(user.Id, user.Roles.Select(r => r.Name).ToList());
                return token;
            }
            else if (username == "client" && password == "password")
            {
                var roles = new List<string> { "Client" };
                var token = _jwtTokenService.GenerateToken(123, roles);
                return token;
            }
            else if (username == "employee" && password == "password")
            {
                var roles = new List<string> { "Employee" };
                var token = _jwtTokenService.GenerateToken(123, roles);
                return token;
            }
            else
            {
                throw new InvalidCredentialsException();
            }

        }
    }
}
