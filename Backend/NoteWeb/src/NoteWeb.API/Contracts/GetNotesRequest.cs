using NoteWeb.Core.Models;

namespace NoteWeb.API.Contracts;

public record GetNotesRequest(
    string? Search,
    string? SortItem,
    string? SortOrder
);

    