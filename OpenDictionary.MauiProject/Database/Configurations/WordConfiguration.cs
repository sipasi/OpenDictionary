using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.HasKey(static entity => entity.Id);

        builder
            .Property(static entity => entity.Origin)
            .IsRequired();

        builder
            .HasOne(static entity => entity.Group)
            .WithMany(static entity => entity.Words)
            .HasForeignKey(static entity => entity.GroupId);
    }
}