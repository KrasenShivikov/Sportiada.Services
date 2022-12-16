namespace Sportiada.Services.Football.Interfaces
{
    using Models.Game;
    using System.Collections.Generic;

    public interface IGameService
    {
        IEnumerable<GameModel> LastTwentyGames();

        GameFinalModel ById(int id);

        IEnumerable<GameWithStatisticModel> GamesBySquadBySeason(int squadId, int seasonId);

        IEnumerable<GameModel> ByTournament(int page, int pageSize, int tournamentId);

        int CountByTournament(int tournamentId);

        IEnumerable<GameModel> ByRound(int roundId);

        IEnumerable<GameModel> ByRounds(IEnumerable<int> roundIds);


    }
}
