using System.ComponentModel.DataAnnotations;

namespace G07_WebAPI.Models;

public record AuthorModel
{
    public int AuthorId { get; init; }

    [Required]
    public string FirstName { get; init; } = null!;

    [Required]
    public string LastName { get; init; } = null!;

    public DateTime? DateOfBirth { get; init; }

    public IEnumerable<int>? BookIds { get; init; }
}
