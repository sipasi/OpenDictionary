using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class SynonymsConfiguration : IEntityTypeConfiguration<Synonyms>
{
    public void Configure(EntityTypeBuilder<Synonyms> builder)
    {
        builder.HasKey(s => s.Id);

        builder
            .Property(s => s.Value)
            .IsRequired();

        builder.HasOne(s => s.Meaning)
            .WithOne(m => m.Synonyms)
            .HasForeignKey<Synonyms>(s => s.MeaningId);
    }
}