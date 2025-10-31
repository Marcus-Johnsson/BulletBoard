using BulletBoard.Data;
using Microsoft.EntityFrameworkCore;
using BulletBoard.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace  BulletBoard.EndPoints.Notes;

public class UpdateNotes
{
    public static void MapEndPoints(IEndpointRouteBuilder app) => app
        .MapPut("/updatenotes/{id}", Handle)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

    private record Request(string  Title, string Description);
    private record Response(int id, string Title, string Description,  string UpdatedAt);
    
    
    private static async Task<IResult> Handle(
        [FromRoute] int id,
        [FromBody] Request request,
        [FromServices] BulletDbContext context)

    {
        try
        {
            var note = await context.Notes.FindAsync(id);
            if (note == null)
                return Results.NotFound();

            if (string.IsNullOrWhiteSpace(request.Title))
                return Results.BadRequest("Title cannot be empty");

            //Update fields
            note.Title = request.Title;
            note.Description = request.Description;
            note.UpdatedAt = DateTime.UtcNow;
        }
        catch
        {
            return Results.NotFound();
        }

        await context.SaveChangesAsync();
            
            return Results.NoContent();
    }
}