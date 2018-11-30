namespace Sportiada.Services.Football.Interfaces
{
    using Models.Season;
    using System.Collections.Generic;

    public interface ISeasonService
    {
        IEnumerable<SeasonModel> All();
    }
}
