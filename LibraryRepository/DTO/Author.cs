using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryRepository.DTO;

public class Author
{
    [Key]
    public int AuthorId { get; set; }

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    public ICollection<Book>? Books { get; set; }
}
