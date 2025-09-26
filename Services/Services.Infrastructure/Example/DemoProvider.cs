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
}
