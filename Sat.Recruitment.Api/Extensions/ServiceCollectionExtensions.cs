using Microsoft.AspNetCore.Builder;
using Sat.Recruitment.Api.Configurations;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
