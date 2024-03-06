using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Net.WebCore.JsonResult;

namespace Net.WebCore.Validator
{
    public class ValidatorActionFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // if validate request body has been error
            if (!context.ModelState.IsValid)
            {
                // get all message key and message content from errors
                var errors = context.ModelState.Where(x => x.Value != null && x.Value.Errors.Any())
                                    .Select(x => ErrorMapping(x))
                                    .ToList();

                context.HttpContext.Response.StatusCode = 400;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsJsonAsync(new {message = "error", errors = errors});
            }
            else
            {
                await next();
            }
        }

        private ErrorModel ErrorMapping(KeyValuePair<string, ModelStateEntry?> error)
        {
            return new ErrorModel(error.Key, error.Value.Errors.Select(x => x.ErrorMessage).FirstOrDefault() ?? "");
        }
    }
}
