using Microsoft.AspNetCore.Builder;
using MyFirstAmazingWebApi.Middlewares;

namespace MyFirstAmazingWebApi.Extensions
{
    public static class Extension
    {
        public static IApplicationBuilder UseHakuna(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HakunaMiddleware>();
        }
    }
}
