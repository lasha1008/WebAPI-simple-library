using AutoMapper;
using G07_WebAPI.Models;
using LibraryRepository;
using LibraryRepository.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace G07_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly LibraryDbContext? _context;

    private IMapper? _mapper;

    public BookController()
    {
        _context = AccessToDbContext.GetDbcontext();
        ConfigurateMapper();
    }

    private void ConfigurateMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BookModel, Book>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.BookId, opt => opt.Ignore());

            cfg.CreateMap<Book, BookModel>()
                .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors.Select(a => a.AuthorId)));
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [HttpGet]
    [Route("retrieve")]
    public IActionResult RetrieveBooks()
    {
        var books = _context!.Books.AsNoTracking().ToList();

        if (books is null)
        {
            return NotFound();
        }

        List<BookModel> bookModels = books
            .Select(x => _mapper!.Map<BookModel>(x))
            .ToList();

        Log.Information("Book retrieved successfully");
        return Ok(bookModels);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateBook(BookModel model)
    {
        if (!ModelState.IsValid)
        {
            Log.Warning("Invalid request: {@Model}", model);
            return BadRequest(ModelState);
        }

        var book = _mapper!.Map<Book>(model);
        var authors = _context!.Authors
            .Where(x => model.AuthorIds!.Contains(x.AuthorId))
            .AsNoTracking()
            .ToArray();

        if (model.AuthorIds!.Count() != authors.Count())
        {
            Log.Warning("Some authors are missing for BookId: {BookId}", model.BookId);
            return BadRequest("Some authors are missing");
        }

        book.Authors = authors;

        _context!.Books.Add(book);
        _context.SaveChanges();

        Log.Information("Book created successfully: {@Book}", book);
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public IActionResult UpdateBook(BookModel model)
    {
        if (!ModelState.IsValid || model.BookId == 0)
        {
            Log.Warning("Invalid request or missing BookId: {@Model}", model);
            return BadRequest(ModelState);
        }

        var book = _mapper!.Map<Book>(model);
        var authors = _context!.Authors
            .Where(x => model.AuthorIds!.Contains(x.AuthorId))
            .AsNoTracking()
            .ToArray();

        if (model.AuthorIds!.Count() != authors.Count())
        {
            return BadRequest("Some authors are missing");
        }

        book.Authors = authors;
        book.BookId = model.BookId;

        _context.Books.Update(book);
        _context.SaveChanges();

        Log.Information("Book updated successfully: {@Book}", book);
        return Ok();
    }
}