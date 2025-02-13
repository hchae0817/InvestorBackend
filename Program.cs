﻿using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=investors.db"));  // SQLite connection string

builder.Services.AddControllers();

var app = builder.Build();

// Initialize the database and import CSV data within a scope
using (var scope = app.Services.CreateScope())  // Creates a scope to resolve scoped services
{
    var services = scope.ServiceProvider;
    await InitializeDatabase(services);  // Call to initialize and import data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

// Initialize the database and import CSV data
async Task InitializeDatabase(IServiceProvider services)
{
    var dbContext = services.GetRequiredService<DatabaseContext>();

    Console.WriteLine("Ensuring database is created...");
    dbContext.Database.Migrate(); 

    if (!dbContext.Investors.Any())  // Check if the database is empty before importing CSV
    {
        var csvFilePath = "data.csv";
        Console.WriteLine($"Checking for CSV file at: {Path.GetFullPath(csvFilePath)}");

        if (File.Exists(csvFilePath))
        {
            Console.WriteLine("CSV file found. Importing data...");
            var lines = await File.ReadAllLinesAsync(csvFilePath);

            foreach (var line in lines.Skip(1)) 
            {
                var columns = line.Split(',');

                if (columns.Length == 8)
                {
                    try
                    {
                        var investor = new Investor
                        {
                            InvestorName = columns[0],
                            InvestorType = columns[1],
                            InvestorCountry = columns[2],
                            InvestorDateAdded = DateTime.Parse(columns[3]),
                            InvestorLastUpdated = DateTime.Parse(columns[4]),
                            CommitmentAssetClass = columns[5],
                            CommitmentAmount = decimal.Parse(columns[6]),
                            CommitmentCurrency = columns[7]
                        };

                        dbContext.Investors.Add(investor);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing CSV line: {line}");
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }

            await dbContext.SaveChangesAsync();
            Console.WriteLine("CSV data imported successfully!");
        }
        else
        {
            Console.WriteLine($"CSV file '{csvFilePath}' NOT found. Ensure it exists in the correct directory.");
        }
    }
    else
    {
        Console.WriteLine("Database already has data. Skipping CSV import.");
    }
}
