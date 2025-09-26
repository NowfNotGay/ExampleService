using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstractions.Example;
using Services.Infrastructure.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Options.DependenciesInjection.WebAppOptions;
public static class WebAppOptions
{
    public static void AddServiceWebAppOptions(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDemoProvider, DemoProvider>();
    }
}
