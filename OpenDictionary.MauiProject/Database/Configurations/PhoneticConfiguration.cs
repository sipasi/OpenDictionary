using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class PhoneticConfiguration : IEntityTypeConfiguration<Phonetic>
{
    public void Configure(EntityTypeBuilder<Phonetic> builder)
    {
        builder.HasKey(static entity => entity.Id);

        builder
            .Property(static entity => entity.Value)
            .IsRequired();

        builder
            .Property(static entity => entity.Audio);

        builder.HasOne(static entity => entity.Metadata)
            .WithMany(static entity => entity.Phonetics)
            .HasForeignKey(static entity => entity.MetadataId);
    }
}