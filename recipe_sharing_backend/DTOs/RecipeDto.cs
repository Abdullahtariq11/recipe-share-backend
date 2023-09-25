using recipe_sharing_backend.Models;
using System.Text.Json.Serialization;

namespace recipe_sharing_backend.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign key for the user who created the recipe

        public int user_tableId { get; set; }


        public UserDto User { get; set; }

        // Foreign key for the category of the recipe
        public int categories_tableId { get; set; }
        
        public CategoryDto Category { get; set; }
    }
}
