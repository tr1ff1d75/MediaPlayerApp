using System.Data.Entity;

namespace MediaPlayerApp
{
    class MediaHistoryContext : DbContext
    {
        public MediaHistoryContext() : base("DefaultConnection")
        {

        }
        public DbSet<MediaHistory> Media { get; set; }
    }
}