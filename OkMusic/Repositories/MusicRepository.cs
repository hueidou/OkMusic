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
    public class MusicRepository
    {
        private readonly OkMusicContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public MusicRepository(OkMusicContext db)
        {
            _db = db;
        }

        internal async Task<List<Music>> GetMusics()
        {
            return await _db.Musics.ToListAsync();
        }

        internal async Task<List<Music>> GetUserMusics(Guid userId)
        {
            return await _db.Musics.Where(x => x.Creator == userId).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        internal async Task<Music> AddMusic(Guid userId, string fileName, string title, double duration)
        {
            var user = await _db.Users.SingleAsync(x => x.UserId == userId);

            var music = new Music
            {
                FileName = fileName,
                Title = title,
                Duration = duration,
                Creator = user.UserId,
                CreateTime = DateTime.Now
            };

            await _db.Musics.AddAsync(music);
            await _db.SaveChangesAsync();

            return music;
        }
    }
}