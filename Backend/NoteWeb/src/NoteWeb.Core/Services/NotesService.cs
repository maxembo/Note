using NoteWeb.Core.Abstractions;
using NoteWeb.Core.Models;

namespace NoteWeb.Core.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _notesRepository;

    public NotesService(INotesRepository notesRepository)
        => _notesRepository = notesRepository;

    public async Task<IReadOnlyCollection<Note>> GetAll()
        => await _notesRepository.GetAll();

    public async Task<IEnumerable<Note>> GetAllNotesSortedOrSearch(string? search, string? sortItem, string? sortOrder) 
        => await _notesRepository.GetAllSearchOrSort(search, sortItem, sortOrder);

    public async Task<Note> Create(Note note)
    {
        if (note.Title.Length > Note.MaxTitleLength)
            throw new ArgumentException(
                $"Length title{note.Title.Length} of the name cannot be more than {Note.MaxTitleLength}");

        if (note.Description.Length > Note.MaxDescriptionLength)
            throw new ArgumentException(
                $"Length title{note.Description.Length} of the name cannot be more than {Note.MaxDescriptionLength}");

        return await _notesRepository.Create(note);
    }

    public async Task<Note?> Update(Guid id, string title, string description, DateTime createdAt)
    {
        var updatedNote = await _notesRepository.Update(id, title, description, createdAt);
        
        if (updatedNote == null)
            throw new ArgumentException($"Id {id} not found");
            
        
        return updatedNote;
    }

    public async Task<Guid> DeleteBy(Guid id)
    {
        var deletedNote = await _notesRepository.DeleteBy(id);
        
        if(deletedNote == Guid.Empty)
            throw new ArgumentException($"Id {id} not found");
        
        return deletedNote;
    }
}