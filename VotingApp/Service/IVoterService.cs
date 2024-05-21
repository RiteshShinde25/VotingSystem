using VotingApp.Model;

namespace VotingApp.Service
{
    public interface IVoterService
    {
        Task<IEnumerable<Voter>> GetVotersAsync();
        Task<Voter> GetVoterByIdAsync(int id);
        Task<Voter> AddEditVoterAsync(Voter voter);
        Task DeleteVoterAsync(int id);
    }
}
