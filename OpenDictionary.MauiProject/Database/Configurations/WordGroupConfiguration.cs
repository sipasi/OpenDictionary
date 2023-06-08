using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class WordGroupConfiguration : IEntityTypeConfiguration<WordGroup>
{
    public void Configure(EntityTypeBuilder<WordGroup> builder)
    {
        builder.HasKey(static entity => entity.Id);

        builder
            .Property(static entity => entity.Name)
            .IsRequired();

        builder
            .Property(static entity => entity.OriginCulture)
            .IsRequired();

        builder
            .Property(static entity => entity.TranslationCulture)
            .IsRequired();

        builder
            .HasMany(static entity => entity.Words)
            .WithOne(static entity => entity.Group)
            .HasForeignKey(static entity => entity.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}