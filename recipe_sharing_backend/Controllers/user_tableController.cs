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
    public class user_tableController : ControllerBase
    {
        private readonly RecipeDbContext _recipeDbContext;

        public user_tableController(RecipeDbContext recipeDbContext)
        {
            _recipeDbContext=recipeDbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users=_recipeDbContext.user.ToList();
            return(Ok(users));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetbyId([FromRoute] int id)
        {
            var users = _recipeDbContext.user.FirstOrDefault(s=>s.Id==id);
            if (users == null) return BadRequest();
            return Ok(users);
           
        }
        //[HttpPost]
        //public IActionResult CreateUser([FromBody] user_table user_Table)
        //{
        //    _recipeDbContext.user.Add(user_Table);
        //    _recipeDbContext.SaveChanges();
        //    return CreatedAtAction(nameof(GetbyId),new {id=user_Table.Id}, user_Table);


        //}
        [HttpPut]
        public IActionResult UpdateUser([FromBody] user_table user_Table)
        {
            var id = user_Table.Id;
            var users = _recipeDbContext.user.FirstOrDefault(s => s.Id == id);
            if (users == null) return BadRequest();
            users.Username = user_Table.Username;
            users.LastName = user_Table.LastName;
            users.Email = user_Table.Email; 
            users.FirstName = user_Table.FirstName;
            return NoContent();

        }
       
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromBody] LoginDto login_request)
        {
            var users = _recipeDbContext.user.FirstOrDefault(u => u.Username == login_request.Username);
            if (users == null) return NotFound("User not found");
            if (users.PasswordHash != login_request.PasswordHash)
            {
                return Unauthorized("Invalid password");
            }
            return Ok(users.Id);


        }


        [HttpPost]
        [Route("signup")]
        public IActionResult SignUpUser([FromBody] UserDto Signup_request)
        {
            try
            {
                 user_table user = new user_table
                {
                    Id = Signup_request.Id,
                    Username = Signup_request.Username,
                    Email = Signup_request.Email,
                    PasswordHash = Signup_request.PasswordHash, // Make sure to hash the password if necessary
                    FirstName = Signup_request.FirstName,
                    LastName = Signup_request.LastName,
                    CreatedAt = Signup_request.CreatedAt,
                    Recipes = new List<recipe_table>() // Initialize with an empty list
                };

                _recipeDbContext.user.Add(user);
                _recipeDbContext.SaveChanges();

                return CreatedAtAction(nameof(GetbyId), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}
