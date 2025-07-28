
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; } 
    public DbSet<Portfolio> Portfolios { get; set; }
    
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.UserId, p.StockId }));
       
        builder.Entity<Portfolio>()
            .HasOne(s => s.Stock)
            .WithMany(s => s.Portfolios)
            .HasForeignKey(s => s.StockId);
        builder.Entity<Portfolio>()
            .HasOne(u => u.User)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(u => u.UserId);
        
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}