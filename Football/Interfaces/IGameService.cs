namespace Sportiada.Services.Football.Interfaces
{
    using Models.Game;
    using System.Collections.Generic;

    public interface IGameService
    {
        IEnumerable<GameModel> LastTwentyGames();

        GameViewModel ById(int id);

        IEnumerable<GameModel> BySeasonIdByCompetition(int seasonId, int competitionId);

        IEnumerable<GameModel> ByRound(int RoundId);
    }
}
