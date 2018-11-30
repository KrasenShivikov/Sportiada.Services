namespace Sportiada.Services.AlpineSki.Implementations
{
    using Data;
    using System.Linq;
    using Interfaces;
    using Models.Skier;
    using Models.Result;
    using Models.Competition;
    using Services.Models;

    public class SkierAlpineSkiService : ISkierAlpineSkiService
    {
        private readonly SportiadaDbContext db;

        public SkierAlpineSkiService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public SkierStandingStatisticModel ById(int id)
        => this.db
                  .Skiers
                  .Where(s => s.Id == id)
                  .Select(s => new SkierStandingStatisticModel
                  {
                      Id = s.Id,
                      Name = s.Name,
                      BirthDate = s.BirthDate,
                      Club = s.Club,
                      Gender = s.Gender,
                      PicturePath = s.LargePicturePath,
                      Country = new CountryModel
                      {
                          Name = s.Country.Name,
                          PicturePath = s.Country.LargePicturePath
                      },
                      Results = s.ResultsAlpineSki
                             .Where(r => r.Stage == ResultAlpineSkiService.Stage|| r.CompetitionsAlpineSki.Discipline.Id == 1 || r.CompetitionsAlpineSki.Discipline.Id == 2)
                             .Select(r => new ResultSkierStatisticAlpineSkiModel
                             {
                                 Id = r.Id,
                                 Place = r.Place,
                                 Stage = r.Stage,
                                 CompetitionsAlpineSki = new CompetitionAlpineSkiModel
                                 {
                                     Id = r.CompetitionsAlpineSki.Id,
                                     CompetitionType = r.CompetitionsAlpineSki.CompetitionType,
                                     DateTime = r.CompetitionsAlpineSki.DateTime,
                                     Discipline = r.CompetitionsAlpineSki.Discipline,
                                     PlaceAlpineSki = r.CompetitionsAlpineSki.PlaceAlpineSki,
                                     Season = r.CompetitionsAlpineSki.Season,
                                     Tournament = r.CompetitionsAlpineSki.Tournament
                                 }
                             }).ToList()
                  })
                  .First();

        public SkierStandingStatisticModel ByIdByTournament(int skierId, int tournamentId)
         => this.db
                  .Skiers
                  .Where(s => s.Id == skierId)
                  .Select(s => new SkierStandingStatisticModel
                  {
                      Id = s.Id,
                      Name = s.Name,
                      BirthDate = s.BirthDate,
                      Club = s.Club,
                      Gender = s.Gender,
                      PicturePath = s.LargePicturePath,
                      Country = new CountryModel
                      {
                          Name = s.Country.Name,
                          PicturePath = s.Country.LargePicturePath
                      },
                      Results = s.ResultsAlpineSki
                             .Where(r => r.CompetitionsAlpineSki.TournamentId == tournamentId 
                             && (r.Stage == ResultAlpineSkiService.Stage || r.CompetitionsAlpineSki.Discipline.Id == 1 || r.CompetitionsAlpineSki.Discipline.Id == 2))
                             .Select(r => new ResultSkierStatisticAlpineSkiModel
                             {
                                 Id = r.Id,
                                 Place = r.Place,
                                 Stage = r.Stage,
                                 CompetitionsAlpineSki = new CompetitionAlpineSkiModel
                                 {
                                     Id = r.CompetitionsAlpineSki.Id,
                                     CompetitionType = r.CompetitionsAlpineSki.CompetitionType,
                                     DateTime = r.CompetitionsAlpineSki.DateTime,
                                     Discipline = r.CompetitionsAlpineSki.Discipline,
                                     PlaceAlpineSki = r.CompetitionsAlpineSki.PlaceAlpineSki,
                                     Season = r.CompetitionsAlpineSki.Season,
                                     Tournament = r.CompetitionsAlpineSki.Tournament
                                 }
                             }).ToList()
                  })
                  .First();
    }
}
