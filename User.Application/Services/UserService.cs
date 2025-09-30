using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models.Response;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserResponseDTO GetUser(int userId)
        {
            //var user = _db.Users.Find(userId);
            var user = new User.Domain.Models.User()
            {
                Username = "client",
                PasswordHash = "password#",
                IsActive = true,
                Id = userId,
                Email = "client@eshop.com"
            };
            if (user == null)
                throw new Exception("Record not found");

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
