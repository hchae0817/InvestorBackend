using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace InvestorApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Investor> Investors { get; set; }
    }
}
