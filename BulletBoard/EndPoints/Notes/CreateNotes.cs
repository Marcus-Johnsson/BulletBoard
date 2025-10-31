using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using BulletBoard.Data;
using BulletBoard.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulletBoard.EndPoints.Notes;

public class CreateNotes
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("api/notes", Handle)
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    
    private record Request(
        string Title,
        string Description
        );

    private record Response(int Id, string Title);
    

    private static async Task<IResult> Handle(
    [FromBody]  Request request,
    [FromServices] BulletDbContext dbContext
    )
    {
        Console.WriteLine("Create Notes");

        var newNote = new Note
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        dbContext.Notes.Add(newNote);
        await dbContext.SaveChangesAsync();
        
        var response = new Response(newNote.Id, newNote.Title);
        return Results.Ok(response);
    }
}
