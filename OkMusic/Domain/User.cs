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
        /// 我的音乐列表
        /// </summary>
        public List<Music> MyMusics { get; set; }
    }
}