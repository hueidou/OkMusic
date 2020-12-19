using System;

namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Music
    {
        /// <summary>
        /// 
        /// </summary>
        public int MusicId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DateTime CreateTime { get;set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Guid Creator { get;set; }
    }
}