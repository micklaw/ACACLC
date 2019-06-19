using System;
using ACACLC.Application.Interfaces;
using ACACLC.Infrastructure.Storage;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ACACLC.DependencyResolution
{
    public static class CommonDependencies
    {
        public static void ConfigureCommon(this IServiceCollection services)
        {
            services.AddSingleton<IStorage, BrowserStorage>();
            services.AddStorage();
        }
    }
}
