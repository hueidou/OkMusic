using System;
using System.Collections.Generic;
using System.IO;
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
    [Route("user/favourite")]
    public class UserFavouriteController : ControllerBase
    {
        private readonly ILogger<UserFavouriteController> _logger;
        private readonly UserRepository _userRepository;
        private readonly MusicFileRepository _musicFileRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        /// <param name="musicFileRepository"></param>
        public UserFavouriteController(ILogger<UserFavouriteController> logger, UserRepository userRepository,
            MusicFileRepository musicFileRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _musicFileRepository = musicFileRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<FavouriteMusic>> Get()
        {
            string userIdStr = Request.Cookies["userId"];
            var userId = Guid.Parse(userIdStr);

            return await _userRepository.GetFavouriteMusics(userId);
        }

        /// <summary>
        /// 上传Music文件
        /// https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
        /// </summary>
        /// <param name="file"></param>
        [HttpPost]
        public async Task Post(IFormFile file)
        {
            string userIdStr = Request.Cookies["userId"];
            var userId = Guid.Parse(userIdStr);

            var fileName = file.FileName;

            await _musicFileRepository.Add(fileName, file.OpenReadStream(), file.ContentType);

            var music = new FavouriteMusic
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                Title = Path.GetFileNameWithoutExtension(fileName),
                UserId = userId
            };
            await _userRepository.AddFavouriteMusic(music);
        }
    }
}
