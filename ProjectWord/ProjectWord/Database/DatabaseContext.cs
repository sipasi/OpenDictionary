
using Microsoft.EntityFrameworkCore;

using ProjectWord.Models;

namespace ProjectWord.AppDatabase
{
    internal class DatabaseContext : DbContext
    {
        private string databasePath;

        public DbSet<Word> Words { get; set; }
        public DbSet<WordGroup> WordGroups { get; set; }

        public DatabaseContext(string databasePath)
        {
            this.databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
