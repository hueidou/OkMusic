using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using OkMusic.Domain;
using OkMusic.Models;

namespace OkMusic.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public class OkHallHub : Hub<IOkHall>
    {
        private readonly OkHall _okHall;
        private readonly OkMusicContext _db;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="okHall"></param>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public OkHallHub(OkHall okHall, OkMusicContext db, IMapper mapper)
        {
            _okHall = okHall;
            _db = db;
            _mapper = mapper;
        }

        private const string groupName = "okhall";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playId"></param>
        /// <returns></returns>
        public async Task Next(Guid playId)
        {
            _okHall.JukeBox.SetCurrent(playId);
            await Clients.Group(groupName).Next(playId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="musicId"></param>
        /// <returns></returns>
        public async Task Push(int musicId)
        {
            var music = _db.Musics.Single(x => x.MusicId == musicId);
            var jukeBoxMusic = _mapper.Map<JukeBoxMusic>(music);

            _okHall.JukeBox.Playlist.Push(jukeBoxMusic);

            await Clients.Group(groupName).Push(jukeBoxMusic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Join()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).NewUserJoin();

            await Clients.Caller.OkHall(_okHall);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string message)
        {
            await Clients.Group(groupName).SendMessage(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await base.OnConnectedAsync();

            // 通知其他人
            await Clients.Group(groupName).NewUserJoin();

            // 连接时，即返回OkHall状态
            await Clients.Caller.OkHall(_okHall);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await base.OnDisconnectedAsync(exception);

            // 通知其他人
            await Clients.Group(groupName).UserLeave();
        }
    }
}