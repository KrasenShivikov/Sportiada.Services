namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class FootballCoachAdminService : IFootballCoachAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballCoachAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballCoachAdminListModel> All()
        {
            var coaches = db.FootballCoaches
                .Select(c => new FootballCoachAdminListModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ShortName = c.ShortName,
                    CountryName = c.Country.Name,
                    CountryPicture = c.Country.PicturePath,
                    Picture = c.Picture
                });

            return coaches;
        }

        public FootballCoachAdminModel ById(int id)
        {
            var coach = db.FootballCoaches
                .Where(c => c.Id == id)
                .Select(c => new FootballCoachAdminModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ShortName = c.ShortName,
                    BirthDate = c.BirthDate.Value,
                    Picture = c.Picture,
                    Description = c.Description,
                    CountryId = c.CountryId,
                    Country = new CountryAdminModel
                    {
                        Name = c.Country.Name,
                        ShortName = c.Country.ShortName,
                        PicturePath = c.Country.PicturePath
                    },
                    Teams = c.Teams
                    .OrderBy(t => t.FromDate)
                    .Select(t => new FootballCoachTeamAdminModel 
                    {
                        Id = t.Id,
                        CoachId = t.CoachId,
                        Team = t.Team,
                        TeamLogo = t.TeamLogo,
                        TeamCountryFlag = t.TeamCountryFlag,
                        FromDate = t.FromDate,
                        UntilDate = t.UntilDate,
                        Position = t.Position,
                        Matches = t.Matches
                    })
                }).FirstOrDefault();

            return coach;
        }

        public void Create(string firstName, string lastName, string shortName, int countryId, DateTime birthDate, string picture, string description)
        {
            FootballCoach coach = new FootballCoach
            {
                FirstName = firstName,
                LastName = lastName,
                ShortName = shortName,
                CountryId = countryId,
                BirthDate = birthDate,
                Picture = picture,
                Description = description
            };

            db.Add(coach);
            db.SaveChanges();
        }

        public void Update(int id, string firstName, string lastName, string shortName, int countryId, DateTime birthDate, string picture, string description)
        {
            FootballCoach coach = GetCoach(id);

            coach.Id = id;
            coach.FirstName = firstName;
            coach.LastName = lastName;
            coach.ShortName = shortName;
            coach.CountryId = countryId;
            coach.BirthDate = birthDate;
            coach.Picture = picture;

            db.Update(coach);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballCoach coach = GetCoach(id);

            db.Remove(coach);
            db.SaveChanges();
        }

       
        public FootballCoach GetCoach(int id)
        {
            FootballCoach coach = db.FootballCoaches
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return coach;
        }
    }
}
