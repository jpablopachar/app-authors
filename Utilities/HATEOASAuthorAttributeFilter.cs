using app_authors.Dtos.Author;
using app_authors.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace app_authors.Utilities
{
    public class HATEOASAuthorAttributeFilter : HATEOASAttributeFilter
    {
        private readonly LinksGenerator _linksGenerator;

        public HATEOASAuthorAttributeFilter(LinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext resultExecutedContext, ResultExecutionDelegate resultExecutionDelegate)
        {
            var mustInclude = MustIncludeHATEOAS(resultExecutedContext);

            if (!mustInclude)
            {
                await resultExecutionDelegate();

                return;
            }

            var result = resultExecutedContext.Result as ObjectResult;
            var authorDto = result!.Value as AuthorDto;

            if (authorDto == null)
            {
                var authorsDto = result.Value as List<AuthorDto> ?? throw new ArgumentException("Se espera una instancia de AuthorDto o List<AuthorDto>.");

                authorsDto.ForEach(async author => await _linksGenerator.GenerateLinks(author));

                result.Value = authorsDto;
            }
            else
            {
                await _linksGenerator.GenerateLinks(authorDto);
            }

            await resultExecutionDelegate();
        }
    }
}