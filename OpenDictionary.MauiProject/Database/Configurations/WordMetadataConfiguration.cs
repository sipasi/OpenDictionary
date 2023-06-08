using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class WordMetadataConfiguration : IEntityTypeConfiguration<WordMetadata>
{
    public void Configure(EntityTypeBuilder<WordMetadata> builder)
    {
        builder.HasKey(static entity => entity.Id);

        builder
            .Property(static entity => entity.Value)
            .IsRequired();

        builder
            .HasMany(static entity => entity.Phonetics)
            .WithOne(static entity => entity.Metadata)
            .HasForeignKey(static entity => entity.MetadataId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(static entity => entity.Meanings)
            .WithOne(static entity => entity.Metadata)
            .HasForeignKey(static entity => entity.MetadataId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}