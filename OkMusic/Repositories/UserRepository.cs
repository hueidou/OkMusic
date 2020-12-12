using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OkMusic.Domain;
using OkMusic.Models;

namespace OkMusic.Repositories 
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository
    {
        private readonly OkMusicContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public UserRepository(OkMusicContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            _db.Users.Update(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal User Get(Guid userId)
        {
            return _db.Users.FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        internal void Add(User user)
        {
            _db.Users.Add(user);
        }
    }
}