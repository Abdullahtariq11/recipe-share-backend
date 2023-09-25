namespace recipe_sharing_backend.Models
{
    public class categories_table
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for recipes associated with this user
        public List<recipe_table> Recipes { get; set; }
    }
}
