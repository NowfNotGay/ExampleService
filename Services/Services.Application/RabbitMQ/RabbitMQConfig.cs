using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Application.RabbitMQ;
public class RabbitMQConfig
{
    public const string RabbitMqRootUri = "rabbitmq://localhost";

    public const string sign = "/";

    public const string RabbitMqUri = RabbitMqRootUri + "/todoQueue";
    public const string RabbitMqLogUri = RabbitMqRootUri + "/LogQueue";
    public const string RabbitMqLogSeqUri = RabbitMqRootUri + "/LogSeqQueue";
    public const string UserName = "guest";
    public const string Password = "guest";
    public const string NameConnection = "Consumer";


    public const string QueueNameDemo = "Demo";
    public const string RabbitMqDemoTopicUri = "Demo.Topic";
    public const string RabbitMqDemoUri = RabbitMqRootUri + sign + QueueNameDemo;
}
