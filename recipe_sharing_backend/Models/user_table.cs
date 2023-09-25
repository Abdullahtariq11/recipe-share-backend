namespace recipe_sharing_backend.Models
{
    public class user_table
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property for recipes associated with this user
        public List<recipe_table> Recipes { get; set; }
    }
}
