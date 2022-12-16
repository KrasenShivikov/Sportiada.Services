namespace Sportiada.Services.AlpineSki.Implementations
{
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using Sportiada.Data;   
    using Models.Competition;
    using Models.Intermediate;
    using Models.Result;
    using Models.Skier;
    using Admin.Models;
    using Services.Models;
    
    public class CompetitionAlpineSkiService : ICompetitionAlpineSkiService
    {
        private readonly SportiadaDbContext db;

        public CompetitionAlpineSkiService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CompetitionAlpineSkiModel> All()
         => this.db
              .CompetitionsAlpineSki
              .Select(c => new CompetitionAlpineSkiModel
              {
                  CompetitionType = c.CompetitionType,
                  DateTime = c.DateTime,
                  Discipline = c.Discipline,
                  Id = c.Id,
                  PlaceAlpineSki = c.PlaceAlpineSki,
                  Season = c.Season,
                  Tournament = c.Tournament
              }).ToList();

        public CompetitionAlpineSkiWithResultsModel ById(int id, string stage)
         => this.db
              .CompetitionsAlpineSki
              .Where(c => c.Id == id)
              .Select(c => new CompetitionAlpineSkiWithResultsModel
              {
                  Id = c.Id,
                  DateTime = c.DateTime,
                  AssistantRef = c.AssistantRef,
                  CompetitionAlpineSkiChief = c.CompetitionAlpineSkiChief,
                  FinishRef = c.FinishRef,
                  FisTechnicalDelegate = c.FisTechnicalDelegate,
                  Referee = c.Referee,
                  StartRef = c.StartRef,
                  CompetitionType = c.CompetitionType,
                  Discipline = c.Discipline,
                  PlaceAlpineSki = c.PlaceAlpineSki,
                  Season = c.Season,
                  Tournament = c.Tournament,
                  Track = c.Track,
                  Results = c.Results.Where(r => r.Stage == stage)
                                     .OrderBy(r => r.Place)
                                     .Select(r => new ResultAlpineSkiModel
                      {
                      Id = r.Id,
                      Bib = r.Bib,
                      Skier = new SkierModel
                      {
                          Id = r.Skier.Id,
                          Name = r.Skier.Name,
                          Country = new CountryAdminModel
                          {
                              Name = r.Skier.Country.Name,
                              ShortName = r.Skier.Country.ShortName,
                              PicturePath = r.Skier.Country.PicturePath,
                          },
                          PicturePath = r.Skier.LargePicturePath
                      },
                      Difference = r.Difference,
                      TimeFirstManch = r.TimeFirstManch,
                      TimeSecondManch = r.TimeSecondManch,
                      FinalTime = r.FinalTime,
                      Metters = r.Metters,
                      Place = r.Place,
                      Stage = r.Stage,
                      StartOrder = r.StartOrder,
                      Intermediates = r.IntermediatesAlpineSki
                                       .AsQueryable()
                                       .Select(i => new IntermediateAlpineSkiModel
                                       {
                                           Name = i.Name,
                                           Place = i.Place,
                                           Difference = i.Difference,
                                           ResultAlpineSki = i.ResultAlpineSki,
                                           Time = i.Time,
                                           Speed = i.Speed
                                       }).ToList()
                  })
                  .ToList()
              })
            .ToList()
            .First();
    }
}
