namespace VotingApp.Model
{
    public class Voter
    {
        public int VoterId { get; set; }
        public string Name { get; set; }
        public bool HasVoted { get; set; } = false;
        public int? CandidateId { get; set; }
        //public Candidate Candidate { get; set; }
    }
}
