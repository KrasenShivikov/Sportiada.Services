namespace Sportiada.Services.Football.Interfaces
{
    using System.Collections.Generic;
    using Models.Round;
    public interface IRoundService
    {
        IEnumerable<RoundBaseModel> ByTournamentBySeason(int tournamentId, int seasonId);

        RoundBaseModel FirstRoundByTournamentBySeason(int tournamentId, int seasonId);
    }
}
