using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using BulletBoard.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulletBoard.EndPoints.Notes;

public class CreateNotes
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/createnotes", Handle)
        .Produces<Response>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    
    private record Request(
        string Title,
        string Description
        );

    

    private static async Task<IResult> Handle(
    [FromBody]  Request request,
    [FromServices] BulletDbContext dbContext
    )

    {
        var newNote = new Note
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.Now
        };
        dbContext.Note.Add(newNote);
        await dbContext.SaveChangesAsync();
        
        var response = new Response(newNote.Id, newNote.Title);
        return Results.Ok(response);
    }
    private record Response(int Id, string Title);//private record Response(int Id, string SupplierName);
}