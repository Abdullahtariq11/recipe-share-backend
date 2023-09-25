using Microsoft.EntityFrameworkCore;
using recipe_sharing_backend.Models;

namespace recipe_sharing_backend.Data
{
    public class RecipeDbContext: DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext>options):base(options) 
        {
        }

        // Add DbSet properties for each model (e.g., Users, Recipes, Categories, etc.)
        public DbSet<categories_table> categories { get; set; }
        public DbSet<comments_table> comments { get; set; }
        public DbSet<rating_table> rating { get; set; }
        public DbSet<user_table> user { get; set; }
        public DbSet<recipe_table> recipe_Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //configure the relationships for this entity.
        {
            modelBuilder.Entity<recipe_table>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(rt => rt.user_tableId);

            modelBuilder.Entity<recipe_table>()
                .HasOne(rt => rt.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(rt => rt.categories_tableId);
        }
    }
}
