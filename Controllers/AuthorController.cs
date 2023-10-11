using app_authors.Utilities;
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
        
    }
}