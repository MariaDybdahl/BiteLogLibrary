using BiteLogLibrary.Helper;
using BiteLogLibrary.Interface.Repository;
using BiteLogLibrary.Interface.Services;
using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using BiteLogLibrary.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<FoodRepository>(new FoodRepository());
builder.Services.AddSingleton<CustomMealFoodRepository>(new CustomMealFoodRepository());
builder.Services.AddSingleton<DailyLogRepository>(new DailyLogRepository());
builder.Services.AddSingleton<CustomMealRepository>(new CustomMealRepository());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
//builder.Services.AddSingleton<CustomPasswordHasher>();
//builder.Services.AddSingleton<IGenericAsyncRepository<Recipe>, RecipeRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Tillad kun denne origin
              .AllowAnyHeader() // Tillad alle headers
              .AllowAnyMethod(); // Tillad alle HTTP-metoder (GET, POST, PUT, DELETE osv.)
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
