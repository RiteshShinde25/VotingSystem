using VotingApp.Model;

namespace VotingApp.Service
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetCandidatesAsync();
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task<Candidate> AddEditCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(int id);
    }
}
