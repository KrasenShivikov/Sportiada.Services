namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FootballCoachTeamAdminService : IFootballCoachTeamAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballCoachTeamAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public void Create(int coachId, string team, string teamLogo, string teamCountryFlag, DateTime fromDate, DateTime untilDate, string position, int matches)
        {
            FootballCoachTeam coachTeam = new FootballCoachTeam
            {
                CoachId = coachId,
                Team = team,
                TeamLogo = teamLogo,
                TeamCountryFlag = teamCountryFlag,
                FromDate = fromDate,
                UntilDate = untilDate,
                Position = position,
                Matches = matches
            };

            db.Add(coachTeam);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballCoachTeam coachTeam = GetCoachTeam(id);

            db.Remove(coachTeam);
            db.SaveChanges();
        }

        public void Update(int id, int coachId, string team, string teamLogo, string teamCountryFlag, DateTime fromDate, DateTime untilDate, string position, int matches)
        {
            FootballCoachTeam coachTeam = GetCoachTeam(id);

            coachTeam.Id = id;
            coachTeam.CoachId = coachId;
            coachTeam.Team = team;
            coachTeam.TeamLogo = teamLogo;
            coachTeam.TeamCountryFlag = teamCountryFlag;
            coachTeam.FromDate = fromDate;
            coachTeam.UntilDate = untilDate;
            coachTeam.Position = position;
            coachTeam.Matches = matches;

            db.Update(coachTeam);
            db.SaveChanges();
        }

        public FootballCoachTeam GetCoachTeam(int id)
        {
            FootballCoachTeam coachTeam = db.FootballCoachTeams
                .Where(ct => ct.Id == id)
                .FirstOrDefault();

            return coachTeam;
        }
    }
}
