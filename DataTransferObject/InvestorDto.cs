namespace Backend.DataTransferObject
{
    public class InvestorDto
    {
        public string? InvestorName { get; set; }
        public List<CommitmentDto> Commitments { get; set; } = new List<CommitmentDto>();
    }
}
