using System;
using System.Threading.Tasks;
using OkMusic.Domain;

namespace OkMusic.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOkHall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playId"></param>
        /// <returns></returns>
        Task Next(Guid playId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task NewUserJoin();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task UserLeave();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="okHall"></param>
        /// <returns></returns>
        Task OkHall(OkHall okHall);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessage(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jukeBoxMusic"></param>
        /// <returns></returns>
        Task Push(JukeBoxMusic jukeBoxMusic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        Task SetMaster(string master);
    }
}