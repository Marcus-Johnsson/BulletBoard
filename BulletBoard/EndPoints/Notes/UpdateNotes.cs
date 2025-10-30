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
        .MapPut("/notes/{id}", Handle)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

    private static async Task<IResult> Handle(
        [FromRoute] int id,
        [FromBody] Note updateNote,
        BulletDbContext db)

    {
        var note = await db.Notes.FindAsync(id);
        if (note == null)
            return Results.NotFound();
        
        if (string.IsNullOrWhiteSpace(updateNote.Title))
            return Results.BadRequest("Title cannot be empty");
        
        //Update fields
        note.Title = updateNote.Title;
        note.Description = updateNote.Description;
        note.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
            
            return Results.NoContent();
    }
}