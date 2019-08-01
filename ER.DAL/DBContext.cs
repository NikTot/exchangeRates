using Microsoft.EntityFrameworkCore;

namespace ER.DAL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Rate> Rates { get; set; }
    }
}
