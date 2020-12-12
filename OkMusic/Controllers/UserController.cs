using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OkMusic.Domain;
using OkMusic.Repositories;

namespace OkMusic.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;
        private readonly MusicFileRepository _musicFileRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        /// <param name="musicFileRepository"></param>
        public UserController(ILogger<UserController> logger, UserRepository userRepository,
            MusicFileRepository musicFileRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _musicFileRepository = musicFileRepository;
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
                var user = new User
                {
                    UserId = userId,
                    UserName = userId.ToString()
                };

                _userRepository.Add(user);

                Response.Cookies.Append("userId", userId.ToString());
                return user;
            }
            else
            {
                return _userRepository.Get(Guid.Parse(userIdStr));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut("{id}")]
        public void Put(Guid id, User user)
        {
            user.UserId = id;
            _userRepository.Update(user);
        }

        /// <summary>
        /// 上传Music文件
        /// https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
        /// </summary>
        /// <param name="file"></param>
        [HttpPost("favourite")]
        public async Task Post(IFormFile file)
        {
            await _musicFileRepository.Add(file.FileName, file.OpenReadStream(), file.ContentType);
        }
    }
}
