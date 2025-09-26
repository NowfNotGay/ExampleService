using MassTransit;
using Services.Domain.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Example;
public class DemoConsumerClone : IConsumer<Demo>
{
    public Task Consume(ConsumeContext<Demo> context)
    {
        return Task.CompletedTask;
    }
}

public class DemoConsumerCloneDefinition : ConsumerDefinition<DemoConsumerClone>
{
    public DemoConsumerCloneDefinition()
    {
        // Override the default endpoint name.
        EndpointName = "DemoClone";
        // Limit the number of messages consumed concurrently
        // to 8, which is the default for a receive endpoint.
        ConcurrentMessageLimit = 16;
    }
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DemoConsumerClone> consumerConfigurator)
    {
        // Configure message retry with intervals
        endpointConfigurator.UseMessageRetry(r => r.Interval(2, 100));
        // Use a simple in-memory outbox to prevent duplicate message delivery
        endpointConfigurator.UseInMemoryOutbox();
    }
}
