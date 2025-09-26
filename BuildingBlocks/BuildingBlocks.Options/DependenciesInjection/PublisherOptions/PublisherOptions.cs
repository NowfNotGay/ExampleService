using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstractions.Example;
using Services.Application.RabbitMQ;
using Services.Infrastructure.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Options.DependenciesInjection.PublisherOptions;
public static class PublisherOptions
{
    public static void AddServicePublisher(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDemoProvider, DemoProvider>();
    }

    public static void AddRabbitMQPublisher(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
           
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(RabbitMQConfig.RabbitMqRootUri), h =>
                {
                    h.Username(RabbitMQConfig.UserName);
                    h.Password(RabbitMQConfig.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
