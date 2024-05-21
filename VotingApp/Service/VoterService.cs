using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Model;

namespace VotingApp.Service
{
    public class VoterService : IVoterService
    {
        private readonly AppDbContext _context;

        public VoterService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voter>> GetVotersAsync()
        {
            try
            {
                //var voters = await _context.Voters.Where(v => !v.HasVoted).ToListAsync();
                var voters = await _context.Voters.ToListAsync();
                return voters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Voter> GetVoterByIdAsync(int id)
        {
            try
            {
                var voter = await _context.Voters.FindAsync(id);
                return voter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Voter> AddEditVoterAsync(Voter voter)
        {
            try
            {
                if (voter.VoterId == 0)
                {
                    _context.Voters.Add(voter);
                }
                else
                {
                    var existingVoter = await _context.Voters.FindAsync(voter.VoterId);
                    if (existingVoter == null)
                    {
                        throw new KeyNotFoundException($"Voter with ID {voter.VoterId} not found.");
                    }
                    existingVoter.Name = voter.Name;
                    existingVoter.HasVoted = voter.HasVoted;
                }

                await _context.SaveChangesAsync();
                return voter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteVoterAsync(int id)
        {
            try
            {
                var voter = await _context.Voters.FindAsync(id);
                if (voter == null)
                {
                    throw new KeyNotFoundException($"Voter with ID {id} not found.");
                }

                _context.Voters.Remove(voter);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}