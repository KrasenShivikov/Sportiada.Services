namespace Sportiada.Services.Football.Interfaces
{
    using Models.Player;

    public interface IPlayerService
    {
        PlayerProfileModel ProfileById(int id);

        PlayerSeasonStatisticModel PlayerSeasonStatistic(int id, int seasonId);
    }
}
