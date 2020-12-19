using System;
using System.ComponentModel.DataAnnotations;

namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class FavouriteMusic
    {
        [Key]
        public int FavouriteMusicId {get;set;}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        public Music Music { get; set; }

        /// <summary>
        /// 收藏时间
        /// </summary>
        /// <value></value>
        public DateTime FavouriteTime { get; set; }
    }
}