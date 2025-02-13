using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Commitment
    {
        [Key]
        public int Id { get; set; }
        public string CommitmentAssetClass { get; set; }
        public decimal CommitmentAmount { get; set; }
        public string CommitmentCurrency { get; set; }

        // Foreign key to Investor
        public int InvestorId { get; set; }
        public Investor Investor { get; set; } // Navigation property
    }
}
