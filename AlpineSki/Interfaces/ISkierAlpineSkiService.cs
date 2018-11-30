namespace Sportiada.Services.AlpineSki.Interfaces
{
    using Models.Skier;

    public interface ISkierAlpineSkiService
    {
        SkierStandingStatisticModel ById(int id);

        SkierStandingStatisticModel ByIdByTournament(int skierId, int tournamentId);
    }
}
