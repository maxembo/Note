namespace NoteWeb.Core.Models;

public class Note
{
    public const int MaxTitleLength = 500;
    public const int MaxDescriptionLength = 1000;

    public Note(Guid id, string title, string description, DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }
}