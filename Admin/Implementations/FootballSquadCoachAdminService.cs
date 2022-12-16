namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Models;
    using Sportiada.Data.Models.Football;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class FootballSquadCoachAdminService : IFootballSquadCoachAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballSquadCoachAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<FootballSquadCoachAdminModel> CoachesBySquadId(int squadId)
        {
            var squadCoach = db.FootballSquadCoaches
                .Where(c => c.SquadId == squadId)
                .Select(c => new FootballSquadCoachAdminModel
                {
                    SquadId = c.SquadId,
                    CoachId = c.CoachId,
                    LeftInSeason = c.LeftInSeason,
                    Position = c.Position,
                    SquadType = c.SquadType,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    Coach = new FootballCoachAdminModel
                    {
                        Id = c.CoachId,
                        FirstName = c.Coach.FirstName,
                        LastName = c.Coach.LastName,
                        ShortName = c.Coach.ShortName,
                        Country = new CountryAdminModel
                        {
                            Name = c.Coach.Country.Name,
                            PicturePath = c.Coach.Country.PicturePath
                        },
                        Picture = c.Coach.Picture
                    }
                });

            return squadCoach;
        }
        public FootballSquadCoachAdminModel BySquadIdByCoachId(int squadId, int coachId)
        {
            var squadCoach = db.FootballSquadCoaches
                .Where(c => c.SquadId == squadId && c.CoachId == coachId)
                .Select(c => new FootballSquadCoachAdminModel
                {
                    SquadId = c.SquadId,
                    CoachId = c.CoachId,
                    Coach = new FootballCoachAdminModel
                    {
                        Id = c.CoachId,
                        FirstName = c.Coach.FirstName,
                        LastName = c.Coach.LastName,
                        ShortName = c.Coach.ShortName,
                        Country = new CountryAdminModel
                        {
                            Name = c.Coach.Country.Name,
                            PicturePath = c.Coach.Country.PicturePath
                        },                     
                        Picture = c.Coach.Picture
                    }
                }).FirstOrDefault();

            return squadCoach;
        }
        public void Create(int squadId, int coachId, string position, string squadType, string fromDate, string toDate, bool leftInSeason)
        {
            FootballSquadCoach squadCoach = new FootballSquadCoach
            {
                SquadId = squadId,
                CoachId = coachId,
                Position = position,
                SquadType = squadType,
                FromDate = fromDate,
                ToDate = toDate,
                LeftInSeason = leftInSeason
            };

            db.Add(squadCoach);
            db.SaveChanges();
        }

        public void Delete(int squadId, int coachId)
        {
            FootballSquadCoach squadCoach = GetSquadCoach(squadId, coachId);

            db.Remove(squadCoach);
            db.SaveChanges();
        }

        public FootballSquadCoach GetSquadCoach(int squadId, int coachId)
        {
            FootballSquadCoach squadCoach = db.FootballSquadCoaches
                            .Where(sc => sc.SquadId == squadId && sc.CoachId == coachId)
                            .FirstOrDefault();

            return squadCoach;
        }
    }
}
