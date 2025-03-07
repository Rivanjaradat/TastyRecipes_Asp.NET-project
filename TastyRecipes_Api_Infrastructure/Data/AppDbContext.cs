using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api_Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships between User and Comments
            builder.Entity<Comments>()
                .HasOne(c => c.User)  // Each Comment is associated with one User
                .WithMany(u => u.Comments)  // A User can have many Comments
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
           
            // Configure relationships between Recipe and Category
            builder.Entity<Recipes>()
                .HasOne(r => r.Category)  // Each Recipe is associated with one Category
                .WithMany(c => c.Recipes)   // A Category can have many Recipes
                .HasForeignKey(r => r.CategoryId); // Foreign key to Category

            // Configure relationships between Recipe and User (if needed)
            builder.Entity<Recipes>()
                .HasOne(r => r.User)  // Each Recipe is associated with one User
                .WithMany(u => u.Recipes)  // A User can have many Recipes
                .HasForeignKey(r => r.UserId);  // Foreign key to User
        }


        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
