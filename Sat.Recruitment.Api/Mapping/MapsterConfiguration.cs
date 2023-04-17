using System.Globalization;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Mapping
{
    public static class MapsterConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<UserDto, User>
                .NewConfig()
                .Map(dest => dest.Money, src => decimal.Parse(src.Money, NumberStyles.Currency));
        }
    }
}
