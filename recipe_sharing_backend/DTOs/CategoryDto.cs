namespace recipe_sharing_backend.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for recipes associated with this user
       public List<RecipeDto> Recipes { get; set; }
    }
}
