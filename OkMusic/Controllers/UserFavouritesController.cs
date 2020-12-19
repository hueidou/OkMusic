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
    [Route("api/user/favourites")]
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
        /// 
        /// </summary>
        /// <param name="musicId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post(int musicId)
        {
            string userIdStr = Request.Cookies["userId"];
            var userId = Guid.Parse(userIdStr);

            await _userRepository.AddFavouriteMusic(userId, musicId);
        }
    }
}
