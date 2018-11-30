namespace Sportiada.Services.AlpineSki.Interfaces
{
    using Models.Competition;
    using System.Collections.Generic;
    
    public interface ICompetitionAlpineSkiService
    {
        IEnumerable<CompetitionAlpineSkiModel> All();

        CompetitionAlpineSkiWithResultsModel ById(int id, string stage);
    }
}
