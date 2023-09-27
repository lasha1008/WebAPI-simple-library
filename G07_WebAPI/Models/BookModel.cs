using LibraryRepository.DTO;
using System.ComponentModel.DataAnnotations;

namespace G07_WebAPI.Models;

public record BookModel
{
    public int BookId { get; init; }

    [Required]
    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public RatingLevel? Rating { get; init; }

    public DateTime? PublishDate { get; init; }

    public bool? IsTaken { get; init; }

    public IEnumerable<int>? AuthorIds { get; init; }
}
