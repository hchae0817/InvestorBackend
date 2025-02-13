namespace Backend.DataTransferObject
{
    public class CommitmentDto
    {
        public string? CommitmentAssetClass { get; set; }
        public decimal CommitmentAmount { get; set; }
        public string? CommitmentCurrency { get; set; }
    }
}
