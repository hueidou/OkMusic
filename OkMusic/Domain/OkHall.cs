using System.Collections.Generic;

namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class OkHall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public List<User> Users { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public JukeBox JukeBox { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void JoinUser(User user)
        {
            Users.Add(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void LeaveUser(User user)
        {
            Users.RemoveAll(x => x.UserId == user.UserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="music"></param>
        public void ChooseMusic(Music music)
        {
            JukeBox.PushMusic(music);
        }
    }

    ///<summary>
    /// 点唱机
    ///</summary>
    public class JukeBox
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        private Stack<Music> Playlist { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Music Current { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="music"></param>
        public void PushMusic(Music music)
        {
            Playlist.Push(music);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Next()
        {
            // 移除当前
            Playlist.Pop();
            Current = Playlist.Peek();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnCurrentEnd()
        {
            Next();
        }
    }
}