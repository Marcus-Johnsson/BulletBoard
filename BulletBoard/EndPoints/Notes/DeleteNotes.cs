using BulletBoard.Data;
using BulletBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BulletBoard.EndPoints.Notes;

public class DeleteNotes
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapDelete("/deletenotes/{id}", Handle)
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

    private static async Task<IResult> Handle(
        [FromRoute] int id,
        [FromServices] BulletDbContext context)
    {
        try
        {
            Console.WriteLine("Del Notes");

            var note = await context.Notes.FindAsync(id);
            
            if (note == null)
            {
                return Results.NotFound($"Note with ID {id} not found.");
            }

            context.Notes.Remove(note);
            await context.SaveChangesAsync();

            return Results.Ok($"Note with ID {id} has been deleted.");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
