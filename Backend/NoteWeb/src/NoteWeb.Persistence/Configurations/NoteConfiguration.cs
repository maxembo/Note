using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteWeb.Core.Models;
using NoteWeb.Persistence.Entities;

namespace NoteWeb.Persistence.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        builder.ToTable("notes");

        builder.HasKey(note => note.Id);
        builder.Property(note => note.Id)
            .HasColumnName("id");
        
        builder.Property(note => note.Title)
            .HasMaxLength(Note.MaxTitleLength)
            .IsRequired()
            .HasColumnName("title");

        builder.Property(note => note.Description)
            .HasMaxLength(Note.MaxDescriptionLength)
            .IsRequired()
            .HasColumnName("description");

        builder.Property(note => note.CreatedAt)
            .HasColumnName("created_at");
    }
}
