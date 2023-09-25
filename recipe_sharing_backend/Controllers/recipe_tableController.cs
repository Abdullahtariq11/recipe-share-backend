using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_sharing_backend.Data;
using recipe_sharing_backend.DTOs;
using recipe_sharing_backend.Models;

namespace recipe_sharing_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class recipe_tableController : ControllerBase
    {

        private readonly RecipeDbContext _dbContext;
        public recipe_tableController(RecipeDbContext recipeDbContext)
        {
            _dbContext = recipeDbContext;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]

        public IActionResult GetAll()
        {
            var recipes= _dbContext.recipe_Tables
                .Select(r=>new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Ingredients = r.Ingredients,
                    CreatedAt = r.CreatedAt,
                    user_tableId = r.user_tableId,
                    categories_tableId = r.categories_tableId,
                    User = new UserDto
                    {
                        Id = r.User.Id,
                        Username = r.User.Username
                        // Add other user properties as needed
                    }
                })
        .ToList();


            if (recipes == null)
            {
                return NotFound();
            }
            return Ok(recipes);
        }


        [HttpGet]
        [Route("api/[controller]/user/{userId:int}")]

        public IActionResult GetByUser([FromRoute] int userId)
        {

            var recipe= _dbContext.recipe_Tables.Where(s=>s.User.Id== userId).ToList();
            if (recipe == null)
            {
                return BadRequest();
            }
            return (Ok(recipe));
        }

        [HttpGet]
        [Route("api/[controller]/category/{category_id:int}")]

        public IActionResult GetByCategory([FromRoute] int category_id)
        {

            var recipe = _dbContext.recipe_Tables.Where(s => s.Category.Id == category_id).ToList();
            if (recipe == null)
            {
                return BadRequest();
            }
            return (Ok(recipe));
        }

        [HttpPost]
        [Route("createRecipe")]

        public IActionResult CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            try
            {
                recipe_table recipe = new recipe_table
                {
                    Id = recipeDto.Id,
                    Title = recipeDto.Title,
                    Description = recipeDto.Description,
                    Ingredients = recipeDto.Ingredients,
                    CreatedAt = recipeDto.CreatedAt,
                    user_tableId = recipeDto.user_tableId,
                    categories_tableId = recipeDto.categories_tableId
                };

                // Assuming _dbContext is your database context
                _dbContext.recipe_Tables.Add(recipe);

                // Find the corresponding User and Category
                var user = _dbContext.user.Find(recipeDto.user_tableId);
                var category = _dbContext.categories.Find(recipeDto.categories_tableId);

                recipe.User = user;
                recipe.Category = category;

                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetByUser), new { id = recipe.Id }, recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


    }
}
