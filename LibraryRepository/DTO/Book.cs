using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryRepository.DTO;

public class Book
{
    [Key]
    public int BookId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "tinyint")]
    public RatingLevel? Rating { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PublishDate { get; set; }

    public bool? IsTaken { get; set; }

    [Required]
    public ICollection<Author> Authors { get; set; } = null!;
}

public enum RatingLevel
{
    Poor = 1,
    Fair = 2,
    Average = 3,
    Good = 4,
    Excellent = 5
}
