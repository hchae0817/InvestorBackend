using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backend.Models;

namespace Backend.Helper
{
    public class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=investors.db;";

        // Create a table if not exists
        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Investors (
                    InvestorName TEXT,
                    InvestorType TEXT,
                    InvestorCountry TEXT,
                    InvestorDateAdded TEXT,
                    InvestorLastUpdated TEXT,
                    CommitmentAssetClass TEXT,
                    CommitmentAmount REAL,
                    CommitmentCurrency TEXT
                )";
            command.ExecuteNonQuery();
        }

        // Insert investor data into the table
        public static void InsertInvestor(Investor investor)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Investors (InvestorName, InvestorType, InvestorCountry, 
                                       InvestorDateAdded, InvestorLastUpdated, CommitmentAssetClass,
                                       CommitmentAmount, CommitmentCurrency)
                VALUES (@InvestorName, @InvestorType, @InvestorCountry, @InvestorDateAdded,
                        @InvestorLastUpdated, @CommitmentAssetClass, @CommitmentAmount, @CommitmentCurrency)";
            command.Parameters.AddWithValue("@InvestorName", investor.InvestorName);
            command.Parameters.AddWithValue("@InvestorType", investor.InvestorType);
            command.Parameters.AddWithValue("@InvestorCountry", investor.InvestorCountry);
            command.Parameters.AddWithValue("@InvestorDateAdded", investor.InvestorDateAdded.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@InvestorLastUpdated", investor.InvestorLastUpdated.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@CommitmentAssetClass", investor.CommitmentAssetClass);
            command.Parameters.AddWithValue("@CommitmentAmount", investor.CommitmentAmount);
            command.Parameters.AddWithValue("@CommitmentCurrency", investor.CommitmentCurrency);

            command.ExecuteNonQuery();
        }

        // Read CSV file and insert data into the database
        public static void ImportCsvData(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Skip(1); // Skip header row

            foreach (var line in lines)
            {
                var columns = line.Split(',');
                if (columns.Length == 8)
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

                    InsertInvestor(investor);
                }
            }
        }
    }
}
