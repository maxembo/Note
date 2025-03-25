using Microsoft.EntityFrameworkCore;
using NoteWeb.Core.Abstractions;
using NoteWeb.Core.Models;
using NoteWeb.Persistence.Entities;

namespace NoteWeb.Persistence.Repositories;

public class NotesRepository : INotesRepository
{
    private readonly NotesDbContext _context;

    public NotesRepository(NotesDbContext context) => _context = context;

    public async Task<IReadOnlyCollection<Note>> GetAll()
    {
        var noteEntity = await _context.Notes
            .AsNoTracking()
            .ToListAsync();

        var notes = noteEntity.Select(n => new Note(n.Id, n.Title, n.Description, n.CreatedAt))
            .ToList();

        return notes;
    }

    public async Task<IEnumerable<Note>> GetAllSearchOrSort(string? search, string? sortItem, string? sortOrder)
    {
        var notesQuery = _context.Notes
            .Where(n =>
            string.IsNullOrWhiteSpace(search) ||
            EF.Functions.ILike(n.Title, $"%{search}%"));
         
        var isDescending = sortOrder == "desc";
        notesQuery = sortItem?.ToLower() switch
        {
            "date" => isDescending
                ? notesQuery.OrderByDescending(n => n.CreatedAt)
                : notesQuery.OrderBy(n => n.CreatedAt),
            "title" => isDescending 
                ? notesQuery.OrderByDescending(n => n.Title) 
                : notesQuery.OrderBy(n => n.Title),
            _ => isDescending 
                ? notesQuery.OrderByDescending(n => n.Id) 
                : notesQuery.OrderBy(n => n.Id)
        };

        var notes = await notesQuery
            .Select(n => new Note(n.Id, n.Title, n.Description, n.CreatedAt))
            .ToListAsync();
        
        return notes;
    }
    
    public async Task<Note> Create(Note note)
    {
        var noteEntity = new NoteEntity() 
        {
            Id = note.Id,
            Title = note.Title,
            Description = note.Description,
            CreatedAt = note.CreatedAt,
        };
        
        await _context.Notes.AddAsync(noteEntity);
        await _context.SaveChangesAsync();
        
        return new Note(noteEntity.Id, noteEntity.Title, noteEntity.Description, noteEntity.CreatedAt);
    }

    public async Task<Note?> Update(Guid id, string title, string description, DateTime createdAt)
    {
        var noteEntity = await _context.Notes
            .Where(n => n.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.Title,title)
                .SetProperty(n => n.Description, description)
                .SetProperty(n => n.CreatedAt, createdAt)
            );
                
        if (noteEntity == 0)
            return null;
        
        return new Note(id, title, description, createdAt);
    }

    public async Task<Guid> DeleteBy(Guid id)
    {
        var noteEntity = await _context.Notes
            .Where(n => n.Id == id)
            .ExecuteDeleteAsync();
        
        if(noteEntity == 0)
            return Guid.Empty;
        
        return id;
    }
}
