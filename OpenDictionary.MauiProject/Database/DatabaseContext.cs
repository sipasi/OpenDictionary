using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class AppDatabaseContext : DatabaseContext
{
    public DbSet<Word> Words { get; set; } = null!;
    public DbSet<WordMetadata> WordMetadatas { get; set; } = null!;
    public DbSet<WordGroup> WordGroups { get; set; } = null!;
    public DbSet<Folder> Folders { get; set; } = null!;

    public AppDatabaseContext(IDatabasePath path) : base(path.Path) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new WordConfiguration());
        builder.ApplyConfiguration(new WordGroupConfiguration());

        builder.ApplyConfiguration(new FolderConfiguration());

        builder.ApplyConfiguration(new WordMetadataConfiguration());
        builder.ApplyConfiguration(new PhoneticConfiguration());
        builder.ApplyConfiguration(new MeaningConfiguration());
        builder.ApplyConfiguration(new SynonymsConfiguration());
        builder.ApplyConfiguration(new AntonymsConfiguration());
    }
}
