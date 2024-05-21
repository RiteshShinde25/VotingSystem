export class IVoterDeatils {
  voterId?: number;
  name?: string;
  hasVoted?: boolean;
  candidateId?: number;
}

export class ICandidateDeatils {
  id?: number;
  name?: string;
  votes?: number;
}
