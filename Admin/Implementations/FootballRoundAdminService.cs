namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FootballRoundAdminService : IFootballRoundAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballRoundAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballRoundAdminModel> RoundsByCompetition(int competitionId)
        {
            var rounds = db.FootballRounds
                         .Where(r => r.CompetitionId == competitionId)
                         .Select(r => new FootballRoundAdminModel
                         {
                             Id = r.Id,
                             Name = r.Name,
                             CompetitionId = r.CompetitionId,
                             CompetitionName = $"{r.Competition.Season.Name} - {r.Competition.Type.Name}"
                         }).ToList();

            return rounds;
        }
        public void Create(string name, int competitionId)
        {
            FootballRound round = new FootballRound();
            round.Name = name;
            round.CompetitionId = competitionId;

            db.Add(round);
            db.SaveChanges();
        }

        public void Update(int id, string name, int competitionId)
        {
            FootballRound round = GetRound(id);
            round.Id = id;
            round.Name = name;
            round.CompetitionId = competitionId;

            db.Update(round);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            FootballRound round = GetRound(id);

            db.Remove(round);
            db.SaveChanges();
        }

        public FootballRound GetRound(int id)
        {
            FootballRound round = db.FootballRounds
                                  .Where(r => r.Id == id)
                                  .FirstOrDefault();

            return round;
        }

        
    }
}
