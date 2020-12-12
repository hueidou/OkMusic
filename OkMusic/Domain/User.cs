using System;
using System.Collections.Generic;

namespace OkMusic.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收藏音乐列表
        /// </summary>
        public List<FavouriteMusic> FavouriteMusics { get; set; }
    }
}