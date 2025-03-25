using NoteWeb.Core.Models;
namespace NoteWeb.Core.Abstractions;

public interface INotesRepository
{
    Task<IReadOnlyCollection<Note>> GetAll();
    Task<IEnumerable<Note>> GetAllSearchOrSort(string? search, string? sortItem, string? sortOrder);
    Task<Note> Create(Note note);
    Task<Note?> Update(Guid id, string title, string description, DateTime createdAt);
    Task<Guid> DeleteBy(Guid id);
}
