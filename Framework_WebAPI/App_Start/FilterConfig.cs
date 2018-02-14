using Framework_WebAPI.Filters;
using System.Web.Http;

namespace Framework_WebAPI
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new JwtAuthenticationAttribute());
        }
    }
}