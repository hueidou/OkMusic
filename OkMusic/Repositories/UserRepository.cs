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

            foreach (var item in user.FavouriteMusics)
            {
                await _db.Entry(item).Reference(x => x.Music).LoadAsync();
            }

            return user.FavouriteMusics;
        }

        internal async Task AddFavouriteMusic(Guid userId, int musicId)
        {
            var music = await _db.Musics.SingleAsync(x => x.MusicId == musicId);

            await AddFavouriteMusic(userId, music);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="music"></param>
        /// <returns></returns>
        internal async Task AddFavouriteMusic(Guid userId, Music music)
        {
            // FavouriteMusic
            var favouriteMusic = new FavouriteMusic
            {
                UserId = userId,
                Music = music,
                FavouriteTime = DateTime.Now
            };

            await _db.FavouriteMusics.AddAsync(favouriteMusic);
            await _db.SaveChangesAsync();
        }
    }
}