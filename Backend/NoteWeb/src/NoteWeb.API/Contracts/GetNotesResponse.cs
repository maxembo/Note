namespace NoteWeb.API.Contracts;

public record GetNotesResponse(IEnumerable<NotesResponse> notes);