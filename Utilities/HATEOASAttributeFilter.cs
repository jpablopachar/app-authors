using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace app_authors.Utilities
{
    public class HATEOASAttributeFilter : ResultFilterAttribute
    {
        protected bool MustIncludeHATEOAS(ResultExecutingContext context)
        {
            var result = context.Result as ObjectResult;

            if (!IsSuccessfulAnswer(result!)) return false;

            var header = context.HttpContext.Request.Headers["includeHATEOAS"];

            if (header.Count == 0) return false;

            var value = header[0];

            if (!value!.Equals("Y", StringComparison.InvariantCultureIgnoreCase)) return false;

            return true;
        }

        private bool IsSuccessfulAnswer(ObjectResult result)
        {
            if (result == null || result.Value == null) return false;

            if (result.StatusCode.HasValue && !result.StatusCode.Value.ToString().StartsWith("2")) return false;

            return true;
        }
    }
}