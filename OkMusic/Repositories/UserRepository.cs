using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _db.SaveChanges();
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
            _db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal async Task<List<FavouriteMusic>> GetFavouriteMusics(Guid userId)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            // https://docs.microsoft.com/zh-cn/ef/core/querying/related-data/explicit
            await _db.Entry(user).Collection(x => x.FavouriteMusics).LoadAsync();

            return user.FavouriteMusics;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="music"></param>
        /// <returns></returns>
        internal async Task AddFavouriteMusic(FavouriteMusic music)
        {
            await _db.FavouriteMusics.AddAsync(music);
            await _db.SaveChangesAsync();
        }
    }
}