using Microsoft.AspNetCore.Builder;
using Sat.Recruitment.Api.Configurations;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
