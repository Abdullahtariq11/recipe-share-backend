using System.Text.Json.Serialization;

namespace recipe_sharing_backend.Models
{
    public class recipe_table
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign key for the user who created the recipe

        public int user_tableId { get; set; }

     
        public user_table User { get; set; }

        // Foreign key for the category of the recipe
        public int categories_tableId { get; set; }
        [JsonIgnore]
        public categories_table Category { get; set; }
    }
}
