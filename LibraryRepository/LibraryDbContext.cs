using LibraryRepository.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryRepository;

public class LibraryDbContext : DbContext
{
    public static IConfiguration? Configuration { get; set; }

    public LibraryDbContext() : base(GetOptions()) { }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;

    private static DbContextOptions GetOptions()
    {
        var connectionString = Configuration!["ConnectionString:LibraryDbConnection"];

        var options = new DbContextOptionsBuilder();
        options.UseSqlServer(connectionString);

        return options.Options;
    }
}