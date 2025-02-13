using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace YourProjectName.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseContext : ControllerBase
    {
        // Get all investors
        [HttpGet]
        public IActionResult GetAllInvestors()
        {
            var investors = new List<Investor>();

            using (var connection = new SqliteConnection("Data Source=investors.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Investors";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        investors.Add(new Investor
                        {
                            InvestorName = reader.GetString(0),
                            InvestorType = reader.GetString(1),
                            InvestorCountry = reader.GetString(2),
                            InvestorDateAdded = reader.GetDateTime(3),
                            InvestorLastUpdated = reader.GetDateTime(4),
                            CommitmentAssetClass = reader.GetString(5),
                            CommitmentAmount = reader.GetDecimal(6),
                            CommitmentCurrency = reader.GetString(7)
                        });
                    }
                }
            }

            return Ok(investors);
        }
    }
}
