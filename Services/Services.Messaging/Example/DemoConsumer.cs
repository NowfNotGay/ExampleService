using MassTransit;
using Services.Application.RabbitMQ;
using Services.Domain.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Example;
public class DemoConsumer : IConsumer<Demo>
{
    public Task Consume(ConsumeContext<Demo> context)
    {
        return Task.CompletedTask;
    }
}

public class DemoConsumerDefinition : ConsumerDefinition<DemoConsumer>
{
    public DemoConsumerDefinition()
    {
        // Override the default endpoint name.
        EndpointName = RabbitMQConfig.QueueNameDemo;
        // Limit the number of messages consumed concurrently
        // to 8, which is the default for a receive endpoint.
        ConcurrentMessageLimit = 16;
    }
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DemoConsumer> consumerConfigurator)
    {
        // Configure message retry with intervals
        endpointConfigurator.UseMessageRetry(r => r.Interval(2, 100));
        // Use a simple in-memory outbox to prevent duplicate message delivery
        endpointConfigurator.UseInMemoryOutbox();
    }
}
