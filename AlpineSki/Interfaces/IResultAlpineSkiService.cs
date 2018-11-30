namespace Sportiada.Services.AlpineSki.Interfaces
{
    using Models.Result;
    using System.Collections.Generic;

    public interface IResultAlpineSkiService
    {
        IEnumerable<ResultCompetitionAlpineSkiModel> AllBySkierByPlaceByTournamentByDiscipline(int skierId, int place, int tournamentId, string disciplineName);

        IEnumerable<ResultCompetitionAlpineSkiModel> AllBySkierByPlaceByDiscipline(int skierId, int place, string disciplineName);
    }
}
