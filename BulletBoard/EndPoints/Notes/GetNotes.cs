using BulletBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BulletBoard.EndPoints.Notes;

public class GetNotes
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/getnotes", Handle)
        .Produces<CreateNotes>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    
private record Request(Note note);
    
private class Response{}

private static async Task<Response> Handle(
    [FromBody]  Request request)
{
    
}
