using G07_WebAPI.Models;
using LibraryRepository;
using LibraryRepository.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace G07_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly LibraryDbContext? _context;

    private IMapper? _mapper;

    public AuthorController()
    {
        _context = AccessToDbContext.GetDbcontext();
        ConfigurateMapper();
    }

    private void ConfigurateMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AuthorModel, Author>()
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.Ignore());

            cfg.CreateMap<Author, AuthorModel>()
                .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books!.Select(b => b.BookId)));
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [HttpGet]
    [Route("retrieve")]
    public IActionResult RetrieveAuthors()
    {
        var authors = _context!.Authors.AsNoTracking();

        List<AuthorModel> authormodels = authors
            .Select(x => _mapper!.Map<AuthorModel>(x))
            .ToList();

        Log.Information("Author retrieved successfully");
        return Ok(authormodels);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateAuthor(AuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            Log.Warning("Invalid request: {@Model}", model);
            return BadRequest(ModelState);
        }

        var author = _mapper!.Map<Author>(model);
        _context!.Authors.Add(author);
        _context.SaveChanges();

        Log.Information("Author created successfully: {@Author}", author);
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public IActionResult UpdateAuthor(AuthorModel model)
    {
        if (!ModelState.IsValid || model.AuthorId == 0)
        {
            Log.Warning("Invalid request or missing AuthorId: {@Model}", model);
            return BadRequest(ModelState);
        }

        var author = _mapper!.Map<Author>(model);

        author.AuthorId = model.AuthorId;

        _context?.Authors.Update(author);
        _context?.SaveChanges();

        Log.Information("Author updated successfully: {@Author}", author);
        return Ok();
    }
}
