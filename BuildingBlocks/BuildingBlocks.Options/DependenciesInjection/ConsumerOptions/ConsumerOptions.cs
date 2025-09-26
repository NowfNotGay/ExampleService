using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstractions.Example;
using Services.Application.RabbitMQ;
using Services.Infrastructure.Example;
using Services.Messaging.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Options.DependenciesInjection.ConsumerOptions;
public static class ConsumerOptions
{
    public static void AddServiceConsumer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDemoProvider, DemoProvider>();
    }


    public static void AddRabbitMQConsumer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            #region DEMO
            x.AddConsumer<DemoConsumer,DemoConsumerDefinition>(); // For Direct
            //x.AddConsumer<DemoConsumerClone,DemoConsumerCloneDefinition>(); // For Fanout both DemoConsumer and DemoConsumerClone will receive the message
            //x.AddConsumer<DemoConsumerTopic,DemoConsumerTopicDefinition>(); // For Topic
            #endregion

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
