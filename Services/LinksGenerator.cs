using app_authors.Dtos;
using app_authors.Dtos.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace app_authors.Services
{
    public class LinksGenerator
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionContextAccessor;

        public LinksGenerator(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
        }

        private IUrlHelper BuildUrlHelper()
        {
            var factory = _httpContextAccessor.HttpContext!.RequestServices.GetRequiredService<IUrlHelperFactory>();

            return factory.GetUrlHelper(_actionContextAccessor.ActionContext!);
        }

        private async Task<bool> IsAdmin()
        {
            var httpContext = _httpContextAccessor.HttpContext!;
            var result = await _authorizationService.AuthorizeAsync(httpContext.User, "isAdmin");

            return result.Succeeded;
        }

        public async Task GenerateLinks(AuthorDto authorDto)
        {
            var isAdmin = await IsAdmin();
            var url = BuildUrlHelper();

            authorDto.Links.Add(new DataHateoas(link: url.Link("GetAuthor", new { id = authorDto.Id })!, description: "self", method: "GET"));

            if (isAdmin)
            {
                authorDto.Links.Add(new DataHateoas(link: url.Link("UpdateAuthor", new { id = authorDto.Id })!, description: "update-author", method: "PUT"));

                authorDto.Links.Add(new DataHateoas(link: url.Link("DeleteAuthor", new { id = authorDto.Id })!, description: "self", method: "DELETE"));
            }
        }
    }
}