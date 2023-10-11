using app_authors.Dtos.Author;
using app_authors.Entities;
using app_authors.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_authors.Controllers
{
    [ApiController]
    // [Route("api/autores")]
    [Route("api/[controller]")]
    // [HeaderIsPresent("x-version", "1")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAuthors")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASAuthorAttributeFilter))]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();

            authors.ForEach(author => author.Name = author.Name!.ToUpper());

            return _mapper.Map<List<AuthorDto>>(authors);
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASAuthorAttributeFilter))]
        public async Task<ActionResult<AuthorBooksDto>> GetAuthorById(int id)
        {
            var author = await _context.Authors.Include(author => author.AuthorBooks)!.ThenInclude(author => author.Book).FirstOrDefaultAsync(author => author.Id == id);

            if (author == null) return NotFound();

            return _mapper.Map<AuthorBooksDto>(author);
        }

        [HttpGet("{name}", Name = "GetAuthorByName")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASAuthorAttributeFilter))]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthorByName([FromRoute] string name)
        {
            var authors = await _context.Authors.Where(author => author.Name!.Contains(name)).ToListAsync();

            return _mapper.Map<List<AuthorDto>>(authors);
        }

        [HttpPost(Name = "CreateAuthor")]
        public async Task<ActionResult> CreateAuthor([FromBody] AuthorRequestDto authorRequestDto)
        {
            var authorExists = await _context.Authors.AnyAsync(author => author.Name == authorRequestDto.Name);

            if (authorExists) return BadRequest($"El autor {authorRequestDto.Name} ya existe.");

            var author = _mapper.Map<AuthorDto>(authorRequestDto);

            _context.Add(author);

            await _context.SaveChangesAsync();

            var authorDto = _mapper.Map<AuthorDto>(author);

            return CreatedAtRoute("GetAuthorById", new { id = authorDto.Id }, authorDto);
        }

        [HttpPut("{id}", Name = "UpdateAuthor")]
        public async Task<ActionResult> UpdateAuthor(AuthorRequestDto authorRequestDto, int id)
        {
            var authorExists = await _context.Authors.AnyAsync(author => author.Id == id);

            if (authorExists) return NotFound();

            var author = _mapper.Map<Author>(authorRequestDto);

            author.Id = id;

            _context.Update(author);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteAuthor")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var authorExists = await _context.Authors.AnyAsync(author => author.Id == id);

            if (authorExists) return NotFound();

            _context.Remove(new Author { Id = id });

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}