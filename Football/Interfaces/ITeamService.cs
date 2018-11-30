namespace Sportiada.Services.Football.Interfaces
{
    using Models.Team;

    public interface ITeamService
    {
        TeamSeasonStatisticModel TeamSeasonStatistic(int seasonId, int teamId);
    }
}
