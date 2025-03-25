using NoteWeb.Core.Models;

namespace NoteWeb.Core.Abstractions;

public interface INotesService
{
    Task<IReadOnlyCollection<Note>> GetAll();
    Task<IEnumerable<Note>> GetAllNotesSortedOrSearch(string? search, string? sortItem, string? sortOrder);
    Task<Note> Create(Note note);
    Task<Note?> Update(Guid id, string title, string description, DateTime createdAt);
    Task<Guid> DeleteBy(Guid id);
}
