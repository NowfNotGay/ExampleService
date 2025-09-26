using MassTransit;
using Services.Domain.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Example;
public class DemoConsumerTopic : IConsumer<Demo>
{
    public Task Consume(ConsumeContext<Demo> context)
    {
        return Task.CompletedTask;
    }
}

public class DemoConsumerTopicDefinition : ConsumerDefinition<DemoConsumerTopic>
{
    public DemoConsumerTopicDefinition()
    {
        // Override the default endpoint name.
        EndpointName = "DemoTopic";
        // Limit the number of messages consumed concurrently
        // to 8, which is the default for a receive endpoint.
        ConcurrentMessageLimit = 16;
    }
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DemoConsumerTopic> consumerConfigurator)
    {
        // Configure message retry with intervals
        endpointConfigurator.UseMessageRetry(r => r.Interval(2, 100));
        // Use a simple in-memory outbox to prevent duplicate message delivery
        endpointConfigurator.UseInMemoryOutbox();

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.Bind<Demo>(s =>
            {
                s.RoutingKey = "Demo.Topic";
                s.ExchangeType = "topic";
            });
        }
    }
}