using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class MeaningConfiguration : IEntityTypeConfiguration<Meaning>
{
    public void Configure(EntityTypeBuilder<Meaning> builder)
    {
        builder.HasKey(static entity => entity.Id);

        builder
            .Property(static entity => entity.PartOfSpeech)
            .IsRequired();

        builder
            .HasMany(static entity => entity.Definitions)
            .WithOne(static entity => entity.Meaning)
            .HasForeignKey(static entity => entity.MeaningId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(static entity => entity.Synonyms)
            .WithOne(static entity => entity.Meaning)
            .HasForeignKey<Synonyms>(static entity => entity.MeaningId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(static entity => entity.Antonyms)
            .WithOne(static entity => entity.Meaning)
            .HasForeignKey<Antonyms>(static entity => entity.MeaningId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(static entity => entity.Metadata)
            .WithMany(static entity => entity.Meanings)
            .HasForeignKey(static entity => entity.MetadataId);
    }
}