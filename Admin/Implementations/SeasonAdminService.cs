namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Interfaces;
    using System.Linq;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SeasonAdminService : ISeasonAdminService
    {
        private readonly SportiadaDbContext db;
        public SeasonAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<SeasonAdminModel> All()
        {

            var seasons = db.Seasons
                              .Select(s => new SeasonAdminModel
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Start = s.Start.Value,
                                  End = s.End.Value
                              });

            return seasons;
        }

        public SeasonAdminModel ById(int id)
        {
            var season = db.Seasons
                             .Where(s => s.Id == id)
                             .Select(s => new SeasonAdminModel
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Start = s.Start.Value,
                                 End = s.End.Value
                             }).FirstOrDefault();

            return season;
        }

        public void Create(string name, DateTime start, DateTime end)
        {
            Season season = new Season
            {
                Name = name,
                Start = start,
                End = end
            };

            db.Add(season);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var season = db.Seasons.Where(s => s.Id == id).FirstOrDefault();
            db.Remove(season);
            db.SaveChanges();
        }

        public void Update(int id, string name, DateTime start, DateTime end)
        {
            var season = this.db.Seasons.Where(s => s.Id == id).FirstOrDefault();
            season.Name = name;
            season.Start = start;
            season.End = end;

            db.Update(season);
            db.SaveChanges();
        }

        public int GetPreviousSeasonId(int id)
        {
            int prevoiousSeasonEndYear = GetPreviousSeasonEndYear(id);
            string prevoiousSeasonEndYearToString = GetPreviousSeasonEndYeartoString(id);

            int previousSeasonId = db.Seasons
                            .Where(s => s.End.Value.Year == prevoiousSeasonEndYear || s.Name.Contains(prevoiousSeasonEndYearToString))
                            .FirstOrDefault()
                            .Id;

            return previousSeasonId;
        }

        private int GetPreviousSeasonEndYear(int id)
        {
            int currentSeasonEndYear = db.Seasons
                        .Where(s => s.Id == id)
                        .FirstOrDefault()
                        .End.Value.Year;

            int prevoiousSeasonEndYear = currentSeasonEndYear - 1;

            return prevoiousSeasonEndYear;
        }

        private string GetPreviousSeasonEndYeartoString(int id)
        {
            string currentSeasonEndYearToString = db.Seasons
                        .Where(s => s.Id == id)
                        .FirstOrDefault()
                        .Name.Substring(5);

            string prevoiousSeasonEndYearToString = (int.Parse(currentSeasonEndYearToString) - 1).ToString();

            return prevoiousSeasonEndYearToString;
        }

    }
}
