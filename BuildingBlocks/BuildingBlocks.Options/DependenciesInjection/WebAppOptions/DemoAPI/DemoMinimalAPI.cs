using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Services.Abstractions.Example;
using Microsoft.AspNetCore.Mvc;
using Services.Domain.Example;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Options.DependenciesInjection.WebAppOptions.DemoAPI;
public static class DemoMinimalAPI
{
    public static void MapDemoMinimalAPI(this WebApplication app)
    {
        var demo = app.MapGroup("/Demo");

        demo.MapGet("/Get", GetDemo);
        demo.MapPost("/Post", PostDemo);
        demo.MapPut("/Put/{id}", PutDemo);
        demo.MapDelete("/Delete/{id}", DeleteDemo);
        demo.MapPatch("/Patch/{id}", PatchDemo);
        demo.MapGet("/GetJson/{id}", GetJsonDemo);
    }

    static IResult GetDemo(IDemoProvider provider)
    {
        var result = provider.Get();
        return TypedResults.Ok(result);
    }

    static IResult PostDemo([FromBody] Demo model, IDemoProvider provider)
    {
        var result = provider.Post(); // nếu muốn truyền model thì điều chỉnh trong provider
        return TypedResults.Ok(result);
    }

    static IResult PutDemo(int id, IDemoProvider provider)
    {
        var result = provider.Put(id);
        return TypedResults.Ok(result);
    }

    static IResult DeleteDemo(int id, IDemoProvider provider)
    {
        var result = provider.Delete(id);
        return TypedResults.Ok(result);
    }

    static IResult PatchDemo(int id, IDemoProvider provider)
    {
        var result = provider.Patch(id);
        return TypedResults.Ok(result);
    }

    static IResult GetJsonDemo(int id, IDemoProvider provider)
    {
        var result = provider.GetJson(id);
        return TypedResults.Ok(result);
    }
}
