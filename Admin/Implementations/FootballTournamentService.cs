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
    public class FootballTournamentService : IFootballTournamentService
    {
        private readonly SportiadaDbContext db;
        public FootballTournamentService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballTournamentAdminModel> All()
        {
            var tournaments = this.db
                .FootballTournamnets
                .Select(ft => new FootballTournamentAdminModel
                {
                    Id = ft.Id,
                    Name = ft.Name,
                    CountryId = ft.CountryId,
                    Country = new CountryAdminModel 
                    {
                        Id = ft.Country.Id,
                        Name = ft.Country.Name,
                        PicturePath = ft.Country.PicturePath,
                        ShortName = ft.Country.ShortName
                    },
                    
                    
                }).ToList();

            return tournaments;
        }

        public FootballTournamentProfileAdminModel ById(int id)
        {
            var tournament = this.db
                .FootballTournamnets
                .Where(ft => ft.Id == id)
                .Select(ft => new FootballTournamentProfileAdminModel
                {
                    Id = ft.Id,
                    Name = ft.Name,
                    CountryId = ft.CountryId,
                    Country = new CountryAdminModel
                    {
                        Id = ft.Country.Id,
                        Name = ft.Country.Name,
                        PicturePath = ft.Country.PicturePath,
                        ShortName = ft.Country.ShortName
                    },
                    Competitions = ft.Competitions.Select(c => new FootballCompetitionAdminModel 
                    {
                        Id = c.Id,
                        CountryName = c.Tournament.Country.Name,
                        SeasonId = c.SeasonId,
                        SeasonName = c.Season.Name,
                        TournamentId = c.TournamentId,
                        TournamentName = c.Tournament.Name,
                        TypeId = c.TypeId,
                        TypeName = c.Type.Name
                        
                    })
                }).FirstOrDefault();

            return tournament;
        }

        public void CreateTournament(string name, int countryId)
        {
            FootballTournament tournament = new FootballTournament
            {
                Name = name,
                CountryId = countryId
            };

            db.Add(tournament);
            db.SaveChanges();
        }

        public void DeleteTournament(int id)
        {
            FootballTournament tournament = GetTournament(id);

            db.Remove(tournament);
            db.SaveChanges();
        }

        public FootballTournamentBaseAdminModel ForDelete(int id)
        {
            var tournament = this.db.FootballTournamnets
                                .Where(t => t.Id == id)
                                .Select(t => new FootballTournamentBaseAdminModel
                                {
                                    Id = t.Id,
                                    Name = t.Name
                                }).FirstOrDefault();

            return tournament;
        }

        public void UpdateTournament(int id, string name, int countryId)
        {
            FootballTournament tournament = GetTournament(id);
            tournament.Name = name;
            tournament.CountryId = countryId;

            db.Update(tournament);
            db.SaveChanges();
        }

        private FootballTournament GetTournament(int id)
        {
            FootballTournament tournament = db
                .FootballTournamnets
                .Where(t => t.Id == id)
                .FirstOrDefault();

            return tournament;
        }
    }
}
