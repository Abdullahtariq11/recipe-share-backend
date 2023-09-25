namespace recipe_sharing_backend.Models
{
    public class comments_table
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign key for the user who wrote the comment
        public int user_tableId { get; set; }
        public user_table User { get; set; }

        // Foreign key for the recipe being commented on
        public int recipe_tableId { get; set; }
        public recipe_table Recipe { get; set; }
    }
}
