using System;
using System.Collections.Generic;

namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDomain
    {
        private readonly IMusicRepository _musicRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="musicRepository"></param>
        public UserDomain(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public List<Music> MyMusics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="music"></param>
        public void Upload(Music music)
        {
            MyMusics.Add(music);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="musicId"></param>
        /// <returns></returns>
        public byte[] Download(int musicId)
        {
            var music = MyMusics.Find(x => x.MusicId == musicId);
            byte[] musicContent = _musicRepository.GetMusic(music.FileName);
            return musicContent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Music> GetMusics()
        {
            return MyMusics;
        }
    }
}