using Services.Domain.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Example;
public interface IDemoProvider
{
    Demo GetDemo();
    string Get();
    string Post();
    string Put(int id);
    string Delete(int id);
    string Patch(int id);
    object GetJson(int id);
}
