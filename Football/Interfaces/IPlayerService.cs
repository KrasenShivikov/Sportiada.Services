namespace Sportiada.Services.Football.Interfaces
{
    using Models.Player;
    using System.Collections.Generic;

    public interface IPlayerService
    {
        PlayerProfileModel ProfileById(int id);

        IEnumerable<PlayerProfileModel> PlayersBySquad(int squadId);

        //PlayerSeasonStatisticModel PlayerSeasonStatistic(int id, int seasonId);
    }
}
