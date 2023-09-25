using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using recipe_sharing_backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//to register dbcontext in dervice
builder.Services.AddDbContext<RecipeDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("RecipeConnectionString"))
);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();



// Enable CORS
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000") // Add your frontend URL here
           .AllowAnyHeader()
           .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Apply CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
