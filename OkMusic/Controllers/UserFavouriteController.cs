using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        [HttpPost("upload")]
        public async Task Post(IFormFile file)
        {
            string userIdStr = Request.Cookies["userId"];
            var userId = Guid.Parse(userIdStr);

            var title = Path.GetFileNameWithoutExtension(file.FileName);
            var fileName = GetStorageFileName(file.FileName);

            await _musicFileRepository.Add(fileName, file.OpenReadStream(), file.ContentType);

            // Music
            var music = new Music
            {
                FileName = fileName,
                Title = title
            };

            await _userRepository.AddFavouriteMusic(userId, music);
        }

        [HttpPost]
        public async Task Post(int musicId)
        {
            string userIdStr = Request.Cookies["userId"];
            var userId = Guid.Parse(userIdStr);

            await _userRepository.AddFavouriteMusic(userId, musicId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetStorageFileName(string fileName)
        {
            // 拼音、ext、日期、随机
            string name = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName).ToLower();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string random = Guid.NewGuid().ToString().Substring(0, 8).ToLower();

            return $"{name}_{date}_{random}{ext}";
        }
    }
}
