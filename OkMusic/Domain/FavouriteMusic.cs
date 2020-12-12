using System;

namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class FavouriteMusic : Music
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }
    }
}