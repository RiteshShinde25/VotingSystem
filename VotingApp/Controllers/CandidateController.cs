using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Model;
using VotingApp.Service;

namespace VotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IVoterService _voterService;

        public CandidateController(ICandidateService candidateService, IVoterService voterService)
        {
            _candidateService = candidateService;
            _voterService = voterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            var candidates = await _candidateService.GetCandidatesAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditCandidate(Candidate candidate)
        {
            try
            {
                var addedCandidate = await _candidateService.AddEditCandidateAsync(candidate);
                return Ok(addedCandidate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("vote")]
        public async Task<IActionResult> SubmitVote(int voterId, int candidateId)
        {
            try
            {
                var voter = await _voterService.GetVoterByIdAsync(voterId);
                var candidate = await _candidateService.GetCandidateByIdAsync(candidateId);

                if (voter == null || candidate == null)
                {
                    return NotFound();
                }
                if (voter.HasVoted)
                {
                    return BadRequest("Voter already voted!!");
                }

                voter.HasVoted = true;
                voter.CandidateId = candidateId;
                await _voterService.AddEditVoterAsync(voter);

                candidate.Votes++;
                await _candidateService.AddEditCandidateAsync(candidate);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            try
            {
                await _candidateService.DeleteCandidateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}