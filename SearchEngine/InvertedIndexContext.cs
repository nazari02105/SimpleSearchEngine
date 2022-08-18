using Microsoft.EntityFrameworkCore;

namespace SearchEngine
{
    public class InvertedIndexContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Document> Documents { get; set; }

        public InvertedIndexContext(DbContextOptions<InvertedIndexContext> options) :
            base(options)
        {
        }
    }
}