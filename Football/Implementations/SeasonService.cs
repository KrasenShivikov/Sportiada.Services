namespace Sportiada.Services.Football.Implementations
{
    
    using Interfaces;
    using Models.Season;
    using Sportiada.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class SeasonService : ISeasonService
    {
        private readonly SportiadaDbContext db;

        public SeasonService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SeasonModel> All()
          => this.db
            .Seasons
            .Select(s => new SeasonModel
            {
                Id = s.Id,
                Name = s.Name
            });
    }
}
