using Services.Abstractions.Example;
using Services.Domain.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Example;
public class DemoProvider : IDemoProvider
{
    public Demo GetDemo()
    {
        return new Demo
        {
            Id = 1,
            Name = "Hồ Trung Nghĩa",
            Payment = 1000,
            BirthDay = new DateTime(2003, 4, 27)
        };
    }

    public string Get() => "Demo Get từ Provider";
    public string Post() => "Demo Post từ Provider";
    public string Put(int id) => $"Demo Put id={id} từ Provider";
    public string Delete(int id) => $"Demo Delete id={id} từ Provider";
    public string Patch(int id) => $"Demo Patch id={id} từ Provider";

    public object GetJson(int id) => new
    {
        Id = id,
        Name = $"Demo {id}",
        Status = "Active",
        CreatedDate = DateTime.UtcNow
    };
}
