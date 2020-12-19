using System;
using System.Collections.Generic;
using System.Linq;

namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class OkHall
    {
        private List<User> users;
        private JukeBox jukeBox;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public List<User> Users
        {
            get
            {
                if (users == null)
                {
                    users = new List<User>();
                }

                return users;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public JukeBox JukeBox
        {
            get
            {
                if (jukeBox == null)
                {
                    jukeBox = new JukeBox();
                }

                return jukeBox;
            }
        }

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

    /// <summary>
    /// 
    /// </summary>
    public class JukeBoxMusic : Music
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Guid PlayId { get; set; }
    }

    ///<summary>
    /// 点唱机
    ///</summary>
    public class JukeBox
    {
        private Stack<JukeBoxMusic> playlist;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Stack<JukeBoxMusic> Playlist
        {
            get
            {
                if (playlist == null)
                {
                    playlist = new Stack<JukeBoxMusic>();
                }

                return playlist;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public JukeBoxMusic Current { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="music"></param>
        public void PushMusic(Music music)
        {
            // TODO
            JukeBoxMusic jukeBoxMusic = (JukeBoxMusic)music;
            Playlist.Push(jukeBoxMusic);
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

        internal void SetCurrent(Guid playId)
        {
            var jukeBoxMusic = Playlist.Single(x => x.PlayId == playId);
            Current = jukeBoxMusic;
        }
    }
}