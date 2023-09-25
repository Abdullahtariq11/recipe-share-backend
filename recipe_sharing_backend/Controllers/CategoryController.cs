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
    public class CategoryController : ControllerBase
    {
        private readonly RecipeDbContext _dbContext;
        public CategoryController(RecipeDbContext dbContext) { 
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("[controller]/GetAll")]
        public IActionResult GetAll()
        {
            var category= _dbContext.categories.Include(c=>c.Recipes).ToList();
            return(Ok(category));
        }

        [HttpGet]
        [Route("[controller]/{id:int}")]
        public IActionResult GetAll([FromRoute] int id)
        {
            var category = _dbContext.categories.FirstOrDefault(c=>c.Id==id);
            if (category == null) return BadRequest();
            return Ok(category);
        }
    }
}
