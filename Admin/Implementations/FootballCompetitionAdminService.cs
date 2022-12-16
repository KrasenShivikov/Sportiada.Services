namespace Sportiada.Services.Admin.Implementations
{
    using Interfaces;
    using Sportiada.Data;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FootballCompetitionAdminService : IFootballCompetitionAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballCompetitionAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballCompetitionAdminModel> CompetitionsByTournamentId(int tournamentId)
        {
            var competitions = db.FootballCompetitions
                            .Where(c => c.TournamentId == tournamentId)
                            .Select(c => new FootballCompetitionAdminModel
                            {
                                Id = c.Id,
                                CountryName = c.Tournament.Country.Name,
                                SeasonName = c.Season.Name,
                                SeasonId = c.SeasonId,
                                TournamentName = c.Tournament.Name,
                                TournamentId = c.TournamentId,
                                TypeName = c.Type.Name,
                                TypeId = c.TypeId
                            });

            return competitions;

        }

        public void Create(int seasonId, int tournamentId, int typeId)
        {
            var competition = new FootballCompetition
            {
                SeasonId = seasonId,
                TournamentId = tournamentId,
                TypeId = typeId
            };

            db.Add(competition);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var competition = GetCompetition(id);
            db.Remove(competition);
            db.SaveChanges();
        }

        public void Update(int id, int seasonId, int tournamentId, int typeId)
        {
            var competition = GetCompetition(id);
            competition.SeasonId = seasonId;
            competition.TournamentId = tournamentId;
            competition.TypeId = typeId;
            db.Update(competition);
            db.SaveChanges();
        }

        private FootballCompetition GetCompetition (int id)
        {
            var competition = db.FootballCompetitions
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return competition;
        }
    }
}
