using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OkMusic.Domain;

namespace OkMusic.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public class OkHallHub : Hub<IOkHall>
    {
        private readonly OkHall _okHall;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="okHall"></param>
        public OkHallHub(OkHall okHall)
        {
            _okHall = okHall;
        }

        private const string groupName = "okhall";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playId"></param>
        /// <returns></returns>
        public async Task Next(int playId)
        {
            await Clients.Group(groupName).Next(playId);
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