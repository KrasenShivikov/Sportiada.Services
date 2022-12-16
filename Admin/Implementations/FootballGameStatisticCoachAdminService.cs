namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class FootballGameStatisticCoachAdminService : IFootballGameStatisticCoachAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballGameStatisticCoachAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballGameStatisticCoachAdminModel ById(int id)
        {
            var coach = db.FootballGameStatisticCoaches
                .Where(c => c.Id == id)
                .Select(c => new FootballGameStatisticCoachAdminModel
                {
                    Id = c.Id,
                    CoachId = c.CoachId,
                    GameStatisticId = c.GameStatisticId,
                    Coach = new FootballCoachAdminListModel
                    {
                        Id = c.Coach.Id,
                        FirstName = c.Coach.FirstName,
                        LastName = c.Coach.LastName,
                        ShortName = c.Coach.ShortName,
                        Picture = c.Coach.Picture,
                        CountryName = c.Coach.Country.Name,
                        CountryPicture = c.Coach.Country.PicturePath
                    }
                }).FirstOrDefault();

            return coach;
        }

        public void Create(int coachId, int gameStatisticId)
        {
            var coach = new FootballGameStatisticCoach
            {
                CoachId = coachId,
                GameStatisticId = gameStatisticId
            };

            db.Add(coach);
            db.SaveChanges();
        }

        public void Update(int id, int coachId, int gameStatisticId)
        {
            var coach = GetGameStatisticCoach(id);

            coach.Id = id;
            coach.CoachId = coachId;
            coach.GameStatisticId = coach.GameStatisticId;

            db.Update(coach);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var coach = GetGameStatisticCoach(id);

            db.Remove(coach);
            db.SaveChanges();
        }

        public FootballGameStatisticCoach GetGameStatisticCoach(int id)
        {
            var coach = db.FootballGameStatisticCoaches
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return coach;
        }


    }
}
