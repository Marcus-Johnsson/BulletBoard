using Microsoft.EntityFrameworkCore;
using BulletBoard.Models;

namespace BulletBoard.Data;

public class BulletDbContext : DbContext
{
    public BulletDbContext(DbContextOptions<BulletDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Note> Notes { get; set; }
}