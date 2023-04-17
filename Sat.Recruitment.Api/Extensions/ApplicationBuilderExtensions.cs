using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Configurations;
using System;
using Mapster;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
