using Backend.Data;
using Backend.DataTransferObject;

namespace Backend.Services
{
    public class InvestorService
    {
        private readonly DatabaseContext _context;

        public InvestorService(DatabaseContext context)
        {
            _context = context;
        }

        public List<InvestorDto> GetInvestorsGroupedByName()
        {
            var groupedInvestors = _context.Investors
                .GroupBy(i => i.InvestorName)
                .Select(g => new InvestorDto
                {
                    InvestorName = g.Key,
                    Commitments = g.Select(i => new CommitmentDto
                    {
                        CommitmentAssetClass = i.CommitmentAssetClass,
                        CommitmentAmount = i.CommitmentAmount,
                        CommitmentCurrency = i.CommitmentCurrency
                    }).ToList()
                })
                .ToList();

            return groupedInvestors;
        }
    }
}
