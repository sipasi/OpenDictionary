using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(static entity => entity.Id);

        builder
            .Property(static entity => entity.Name)
            .IsRequired();

        builder
            .HasMany(static entity => entity.Groups)
            .WithOne(static entity => entity.Folder)
            .HasForeignKey(static entity => entity.FolderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(static entity => entity.Parent)
            .WithMany(static entity => entity.Subfolders)
            .HasForeignKey(static entity => entity.ParentId);
    }
}