using Microsoft.AspNetCore.Mvc;
using NoteWeb.API.Contracts;
using NoteWeb.Core.Abstractions;
using NoteWeb.Core.Models;
using NoteWeb.Persistence;

namespace NoteWeb.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;

    public NotesController(INotesService notesService) => _notesService = notesService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NotesRequest request)
    {
        
        var note = new Note(Guid.NewGuid(), request.Title, request.Description, DateTime.UtcNow);

        try
        {
            var createdNotes = await _notesService.Create(note);
            
            return Ok(createdNotes);
        }
        catch (ArgumentException argument)
        {
            return BadRequest(argument.Message);
        }
    }
    
     [HttpGet]
     public async Task<ActionResult<IEnumerable<NotesResponse>>> Get([FromQuery] GetNotesRequest request)
     {
         var notes
             = await _notesService.GetAllNotesSortedOrSearch(request.Search, request.SortItem, request.SortOrder);

         var response
             = notes
                 .Select(n => new NotesResponse(n.Id, n.Title, n.Description, n.CreatedAt));
         
         return Ok(new GetNotesResponse(response));
     }
    
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] NotesRequest request)
    {
        try
        {
            var updatedNote = await _notesService.Update(id, request.Title, request.Description, DateTime.UtcNow);
            return Ok(updatedNote);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBy(Guid id)
    {
        try
        {
            var deletedNote = await _notesService.DeleteBy(id);
            return Ok(deletedNote);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
}