using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Model;

namespace VotingApp.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _context;

        public CandidateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
        {
            try
            {
                var candidates = await _context.Candidates.ToListAsync();
                return candidates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            try
            {
                var candidate = await _context.Candidates.FindAsync(id);
                return candidate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Candidate> AddEditCandidateAsync(Candidate candidate)
        {
            try
            {
                if (candidate.Id == 0)
                {
                    _context.Candidates.Add(candidate);
                }
                else
                {
                    var existingCandidate = await _context.Candidates.FindAsync(candidate.Id);
                    if (existingCandidate == null)
                    {
                        throw new KeyNotFoundException($"Candidate with ID {candidate.Id} not found.");
                    }
                    existingCandidate.Name = candidate.Name;
                    existingCandidate.Votes = candidate.Votes;
                }

                await _context.SaveChangesAsync();
                return candidate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCandidateAsync(int id)
        {
            try
            {
                var candidate = await _context.Candidates.FindAsync(id);
                if (candidate == null)
                {
                    throw new KeyNotFoundException($"Candidate with ID {id} not found.");
                }

                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}