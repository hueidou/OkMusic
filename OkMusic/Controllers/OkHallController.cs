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
    [Route("api/[controller]")]
    public class OkHallController : ControllerBase
    {
        private readonly ILogger<OkHallController> _logger;
        private readonly UserRepository _userRepository;
        private readonly MusicRepository _musicRepository;
        private readonly OkHall _okHall;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        /// <param name="musicRepository"></param>
        /// <param name="okHall"></param>
        public OkHallController(ILogger<OkHallController> logger, UserRepository userRepository,
            MusicRepository musicRepository,
            OkHall okHall)
        {
            _logger = logger;
            _userRepository = userRepository;
            _musicRepository = musicRepository;
            _okHall = okHall;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<OkHall> Get()
        {
            return _okHall;
        }
    }
}