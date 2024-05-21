using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Model;
using VotingApp.Service;

namespace VotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        private readonly IVoterService _voterService;

        public VoterController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voter>>> GetVoters()
        {
            try
            {
                var voters = await _voterService.GetVotersAsync();
                return Ok(voters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Voter>> GetVoter(int id)
        {
            try
            {
                var voter = await _voterService.GetVoterByIdAsync(id);
                if (voter == null)
                {
                    return NotFound();
                }
                return Ok(voter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEditVoter(Voter voter)
        {
            try
            {
                var addedVoter = await _voterService.AddEditVoterAsync(voter);
                return Ok(addedVoter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            try
            {
                await _voterService.DeleteVoterAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}