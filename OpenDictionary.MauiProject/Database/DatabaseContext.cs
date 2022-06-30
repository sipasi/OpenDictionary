
using Microsoft.EntityFrameworkCore;


using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase;

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

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite($"Filename={databasePath}");

        builder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<WordGroup>()
            .HasMany(entity => entity.Words)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Entity<WordMetadata>()
            .HasMany(entity => entity.Phonetics)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .Entity<WordMetadata>()
            .HasMany(entity => entity.Meanings)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .Entity<Meaning>()
            .HasMany(entity => entity.Definitions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Entity<Meaning>()
            .HasOne(entity => entity.Synonyms)
            .WithOne()
            .HasForeignKey<Synonyms>(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .Entity<Meaning>()
            .HasOne(entity => entity.Antonyms)
            .WithOne()
            .HasForeignKey<Antonyms>(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}