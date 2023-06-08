using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class AntonymsConfiguration : IEntityTypeConfiguration<Antonyms>
{
    public void Configure(EntityTypeBuilder<Antonyms> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .Property(a => a.Value)
            .IsRequired();

        builder
            .HasOne(a => a.Meaning)
            .WithOne(m => m.Antonyms)
            .HasForeignKey<Antonyms>(a => a.MeaningId);
    }
}