
using Microsoft.EntityFrameworkCore;

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

            public DbSet<User> Users { get; set; }
    }
}
}