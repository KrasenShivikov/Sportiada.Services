namespace Sportiada.Services.Football.Implementations
{
    using System.Collections.Generic;
    using Interfaces;
    using System.Linq;
    using Sportiada.Data;
    using Sportiada.Services.Football.Models.Round;

    public class RoundService : IRoundService
    {   
        private readonly SportiadaDbContext db;
        public RoundService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<RoundBaseModel> ByTournamentBySeason(int tournamentId, int seasonId)
         => this.db
              .FootballRounds
              .Where(r => r.Competition.TournamentId == tournamentId
                    && r.Competition.SeasonId == seasonId)
              .Select(r => new RoundBaseModel
              {
                  Id = r.Id,
                  Name = r.Name
              }).ToList();

        public RoundBaseModel FirstRoundByTournamentBySeason(int tournamentId, int seasonId)
        => this.ByTournamentBySeason(tournamentId, seasonId).First();
    }
}
