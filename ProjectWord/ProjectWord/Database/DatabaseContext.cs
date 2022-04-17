
using Microsoft.EntityFrameworkCore;


using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase
{
    internal class DatabaseContext : DbContext
    {
        private readonly string databasePath;

        public DbSet<Word> Words { get; set; }
        public DbSet<WordMetadata> WordMetadatas { get; set; }
        public DbSet<WordGroup> WordGroups { get; set; }

        public DatabaseContext(string databasePath)
        {
            this.databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<WordMetadata>().HasKey(metadata => metadata.Id);

            builder
                .Entity<WordMetadata>()
                .HasMany(entity => entity.Phonetics);
            builder
                .Entity<WordMetadata>()
                .HasMany(entity => entity.Meanings);
            builder
                .Entity<Meaning>()
                .HasMany(entity => entity.Definitions);
            builder
                .Entity<Definition>()
                .HasMany(entity => entity.Synonyms);
            builder
                .Entity<Definition>()
                .HasMany(entity => entity.Antonyms);
        }
    }
}