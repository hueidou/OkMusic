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
    public class MusicsController : ControllerBase
    {
        private readonly ILogger<MusicsController> _logger;
        private readonly UserRepository _userRepository;
        private readonly MusicRepository _musicRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        /// <param name="musicRepository"></param>
        public MusicsController(ILogger<MusicsController> logger, UserRepository userRepository,
            MusicRepository musicRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _musicRepository = musicRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Music>> Get()
        {
            return await _musicRepository.GetMusics();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<List<Music>> GetUserMusics(Guid userId)
        {
            return await _musicRepository.GetUserMusics(userId);
        }
    }
}