using Backend.Data;
using Backend.Helper;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=investors.db"));  // SQLite connection string

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyMethod() 
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<InvestorService>();

var app = builder.Build();

app.UseCors("AllowAll");
// Initialize the database and import CSV data within a scope
using (var scope = app.Services.CreateScope())  // Creates a scope to resolve scoped services
{
    var services = scope.ServiceProvider;

    await DatabaseHelper.InitializeDatabase(services);  // Call to initialize and import data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

