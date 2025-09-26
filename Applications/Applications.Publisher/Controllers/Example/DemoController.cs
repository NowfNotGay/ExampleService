using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Application.RabbitMQ;
using Services.Domain.Example;

namespace Applications.Publisher.Controllers.Example;

[Route("RabbitMQ/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly IBus _bus;
    public DemoController(IBus bus)
    {
        _bus = bus;
    }

    //Fanout Exchange (Broadcast, tất cả queue đều nhận)
    [HttpGet("SendDemoMessageDefault")]
    public async Task<IActionResult> SendDemoMessageDefault()
    {
        var demo = new Demo
        {
            Id = 1,
            Name = "Hồ Trung Nghĩa",
            Payment = 1000,
            BirthDay = new DateTime(2003, 4, 27)
        };
        await _bus.Publish<Demo>(demo);
        return Ok("Message sent to the RabbitMQ topic exchange successfully");
    }


    //Direct Exchange (Point-to-Point, Command)
    [HttpGet("SendDemoMessage")]
    public async Task<IActionResult> SendDemoMessage()
    {
        var demo = new Demo
        {
            Id = 1,
            Name = "Hồ Trung Nghĩa",
            Payment = 1000,
            BirthDay = new DateTime(2003, 4, 27)
        };
        Uri uri = new Uri(RabbitMQConfig.RabbitMqDemoUri);
        var endPoint = await _bus.GetSendEndpoint(uri);
        return Ok("Message sent to the RabbitMQ topic exchange successfully");
    }

    //Topic Exchange (Publish-Subscribe, Event)
    [HttpGet("SendDemoMessageTopic")]
    public async Task<IActionResult> SendDemoMessageTopic()
    {
        var demo = new Demo
        {
            Id = 1,
            Name = "Hồ Trung Nghĩa",
            Payment = 1000,
            BirthDay = new DateTime(2003, 4, 27)
        };
        Uri uri = new Uri(string.Format("exchange:{0}", RabbitMQConfig.RabbitMqDemoTopicUri));
        var endPoint = await _bus.GetSendEndpoint(uri);
        await endPoint.Send<Demo>(demo,ctx =>
        {
            ctx.SetRoutingKey(RabbitMQConfig.RabbitMqDemoTopicUri);
        });
        return Ok("Message sent to the RabbitMQ topic exchange successfully");
    }

}
