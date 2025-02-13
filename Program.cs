using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=investors.db"));  // SQLite connection string

builder.Services.AddControllers();

var app = builder.Build();

// Initialize the database and import CSV data
await InitializeDatabase(app.Services);

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
    dbContext.Database.EnsureCreated(); // Ensure the database is created

    // Check if the database already has data
    if (!dbContext.Investors.Any())
    {
        // Path to your CSV file
        var csvFilePath = "data.csv";  // Make sure this file is in the correct directory

        // Check if the CSV file exists
        if (File.Exists(csvFilePath))
        {
            var lines = File.ReadAllLines(csvFilePath).Skip(1); // Skip the header row

            // Loop through each line in the CSV file and insert it into the database
            foreach (var line in lines)
            {
                var columns = line.Split(',');

                if (columns.Length == 8) // Ensure that the line has the expected number of columns
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

                    // Add the investor to the context
                    dbContext.Investors.Add(investor);
                }
            }

            // Save all changes to the database
            await dbContext.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine($"CSV file '{csvFilePath}' not found.");
        }
    }
}
