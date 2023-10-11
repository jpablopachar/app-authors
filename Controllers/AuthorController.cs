using app_authors.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app_authors.Controllers
{
    [ApiController]
    [Route("api/autores")]
    [HeaderIsPresent("x-version", "1")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public AuthorController(AppDbContext context, IMapper mapper, IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }
    }
}