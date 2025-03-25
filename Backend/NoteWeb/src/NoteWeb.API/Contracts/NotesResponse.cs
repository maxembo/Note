namespace NoteWeb.API.Contracts;

public record NotesResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime CreatedAt
);