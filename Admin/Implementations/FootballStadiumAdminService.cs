namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class FootballStadiumAdminService : IFootballStadiumAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballStadiumAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballStadiumAdminModel ById(int id)
        {
            var stadium = db.FootballStadiums
                .Where(s => s.Id == id)
                .Select(s => new FootballStadiumAdminModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    TeamId = s.TeamId,
                    Team = new FootballTeamAdminModel
                    {
                        Id = s.Team.Id,
                        Name = s.Team.Name,
                        City = new CityAdminModel
                        {
                            Name = s.Team.City.Name,
                            Country = new CountryAdminModel
                            {
                                PicturePath = s.Team.City.Country.PicturePath
                            }
                        }
                    }
                }).FirstOrDefault();

            return stadium;
        }

        public void Create(int teamId, string name)
        {
            FootballStadium stadium = new FootballStadium
            {
                TeamId = teamId,
                Name = name
            };

            db.Add(stadium);
            db.SaveChanges();
        }

        public void Update(int id, int teamId, string name)
        {
            var stadium = GetFootballStadium(id);

            stadium.Id = id;
            stadium.TeamId = teamId;
            stadium.Name = name;

            db.Update(stadium);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var stadium = GetFootballStadium(id);

            db.Remove(stadium);
            db.SaveChanges();
        }

        public FootballStadium GetFootballStadium(int id)
        {
            var stadium = db.FootballStadiums
                .Where(s => s.Id == id)
                .FirstOrDefault();

            return stadium;        
        }

        
    }
}
