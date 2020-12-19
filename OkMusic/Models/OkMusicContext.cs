
using Microsoft.EntityFrameworkCore;
using OkMusic.Domain;

namespace OkMusic.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OkMusicContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public OkMusicContext(DbContextOptions<OkMusicContext> options)
          : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DbSet<Music> Musics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DbSet<FavouriteMusic> FavouriteMusics { get; set; }
    }
}