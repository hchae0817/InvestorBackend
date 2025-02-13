using Backend.Data;
using Backend.DataTransferObject;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorsController : ControllerBase
    {
        private readonly InvestorService _investorService;

        public InvestorsController(InvestorService investorService)
        {
            _investorService = investorService;
        }

        // GET: api/investors
        [HttpGet]
        public ActionResult<List<InvestorDto>> GetInvestorsGroupedByName()
        {
            try
            {
                var result = _investorService.GetInvestorsGroupedByName();

                if (result == null || !result.Any())
                {
                    return NotFound("No investors found.");
                }

                return Ok(result); // Returns 200 OK with the result as JSON
            }
            catch (Exception ex)
            {
                // Optionally, log the exception or return a 500 error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
