namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class FootballTeamAdminService : IFootballTeamAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballTeamAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballTeamAdminModel> All()
          => this.db.FootballTeams
                    .Select(t => new FootballTeamAdminModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                        City = new CityAdminModel 
                        { 
                            Id = t.CityId,
                            Name = t.City.Name,
                            Country = new CountryAdminModel
                            { 
                                Id = t.City.CountryId,
                                Name = t.City.Country.Name,
                                PicturePath = t.City.Country.PicturePath,
                                ShortName = t.City.Country.ShortName
                            }
                        }
                    });

        public FootballTeamAdminModel ById(int id)
          => this.db.FootballTeams
                   .Where(t => t.Id == id)
                   .Select(t => new FootballTeamAdminModel
                   {
                       Name = t.Name,
                       Id = t.Id,
                       Logo = t.Logo,
                       City = new CityAdminModel
                       {
                           Id = t.CityId,
                           Name = t.City.Name,
                           Country = new CountryAdminModel 
                           {
                               Name = t.City.Country.Name,
                               PicturePath = t.City.Country.PicturePath
                           }

                       },
                       Squads = t.Squads.Select(s => new FootballSquadAdminModel 
                       {
                           Id = s.Id,
                           SeasonId = s.SeasonId,
                           Season = new SeasonAdminModel
                           {
                               Name = s.Season.Name
                           }    
                       }),
                       Stadiums = t.Stadiums.Select(s => new FootballStadiumAdminModel
                       {
                           Id = s.Id,
                           Name = s.Name
                       })
                   }).FirstOrDefault();

        public void CreateFootballTeam(string name, int cityId, string logo)
        {
            FootballTeam team = new FootballTeam
            {
                Name = name,
                CityId = cityId,
                Logo = logo
            };

            this.db.Add(team);
            this.db.SaveChanges();
        }

        public void UpdateFootballTeam(int id, int cityId, string name, string logo)
        {
            FootballTeam team = this.db.FootballTeams.Where(t => t.Id == id).FirstOrDefault();
            team.Id = id;
            team.CityId = cityId;
            team.Name = name;
            team.Logo = logo;
            this.db.Update(team);
            this.db.SaveChanges();
        }

        public void DeleteFootballTeam(int id)
        {
            var team = this.db.FootballTeams
                           .Where(t => t.Id == id)
                           .FirstOrDefault();

            this.db.Remove(team);
            this.db.SaveChanges();
        }

        
    }
}
