using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OkMusic.Common;
using OkMusic.Domain;
using OkMusic.Repositories;

namespace OkMusic.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("user/musics")]
    public class UserMusicsController : ControllerBase
    {
        private readonly ILogger<UserMusicsController> _logger;
        private readonly UserRepository _userRepository;
        private readonly MusicFileRepository _musicFileRepository;
        private readonly MusicRepository _musicRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        /// <param name="musicFileRepository"></param>
        /// <param name="musicRepository"></param>
        public UserMusicsController(ILogger<UserMusicsController> logger, UserRepository userRepository,
            MusicFileRepository musicFileRepository,
            MusicRepository musicRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _musicFileRepository = musicFileRepository;
            _musicRepository = musicRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Music>> Get()
        {
            string userIdStr = Request.Cookies["userId"];
            var userId = Guid.Parse(userIdStr);

            return await _musicRepository.GetUserMusics(userId);
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

            var fileStream = file.OpenReadStream();
            await _musicFileRepository.Add(fileName, fileStream, file.ContentType);

            //
            var tagFile = TagLib.File.Create(new StreamFileAbstraction { Name = file.FileName, ReadStream = file.OpenReadStream() });
            var duration = tagFile.Properties.Duration.TotalMilliseconds;

            var music = await _musicRepository.AddMusic(userId, fileName, title, duration);
            await _userRepository.AddFavouriteMusic(userId, music);
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
