using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Investor
    {
        [Key] 
        public int Id { get; set; }
        public string? InvestorName { get; set; }
        public string? InvestorType { get; set; }
        public string? InvestorCountry { get; set; }
        public DateTime InvestorDateAdded { get; set; }
        public DateTime InvestorLastUpdated { get; set; }
        public string? CommitmentAssetClass { get; set; }
        public decimal CommitmentAmount { get; set; }
        public string?   CommitmentCurrency { get; set; }
    }
}
