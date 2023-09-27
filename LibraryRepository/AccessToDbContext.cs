namespace LibraryRepository;

public class AccessToDbContext
{
    private static readonly LibraryDbContext _instance = new LibraryDbContext();

    private AccessToDbContext() { }

    public static LibraryDbContext GetDbcontext() => _instance;
}
