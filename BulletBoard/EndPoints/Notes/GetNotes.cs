using BulletBoard.Data;
using BulletBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BulletBoard.EndPoints.Notes;

public class GetNotes
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/notes", Handle)
        .Produces<GetNotes>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    private class Response(List<Note> Notes);


    private static async Task<IResult> Handle(
        [FromServices] BulletDbContext context)
    {
        Console.WriteLine("Get Notes");
        try
        {
            var notes = await context.Notes.ToListAsync();
            return Results.Ok(notes);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
