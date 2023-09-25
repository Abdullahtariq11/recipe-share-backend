namespace recipe_sharing_backend.Models
{
    public class rating_table
    {
        public int Id { get; set; }
        public int Value { get; set; }

        //Foreign key for the user who rated the recipe
        public int user_tableId { get; set; }
        public user_table user { get; set; }
        //Foreign key for the recipe being rated
        public recipe_table recipe_table { get; set; }  
        public int recipe_tableId { get; set; }

    }
}
