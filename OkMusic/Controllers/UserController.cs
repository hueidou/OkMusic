using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OkMusic.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;

        public UserController(ILogger<UserController> logger, UserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public User Get()
        {
            string userIdStr = Request.Cookies["userId"];
            if (string.IsNullOrEmpty(userIdStr))
            {
                Guid userId = Guid.NewGuid();
                _userRepository.Add(new User{
                    UserId = userId,
                    UserName = userId.ToString()
                });

                Response.Cookies.Append("userId", userId);

            }
            else
            {
                return _userRepository.Get(Guid.Parse(userIdStr));
            }
        }

        [HttpPut("{id}")]
        public void Put(Guid id, User user)
        {
            user.UserId = id;
            _userRepository.Update(user);
        }
    }
}
