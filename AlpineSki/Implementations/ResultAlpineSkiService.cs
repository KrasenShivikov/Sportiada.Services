namespace Sportiada.Services.AlpineSki.Implementations
{
    using AutoMapper;
    using Data;
    using Interfaces;
    using Models.Result;
    using Models.Competition;
    using Models.Skier;
    using Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    

    public class ResultAlpineSkiService : IResultAlpineSkiService
    {
        private readonly SportiadaDbContext db;
        private readonly IMapper mapper;

        public const string Stage = "ОБЩО";

        public ResultAlpineSkiService(SportiadaDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<ResultCompetitionAlpineSkiModel> AllBySkierByPlaceByDiscipline(int skierId, int place, string disciplineName)
            => this.db
               .ResultsAlpineSki
               .Where(r => r.SkierId == skierId && r.Place == place && r.CompetitionsAlpineSki.Discipline.Name == disciplineName
                && (r.Stage == Stage || r.CompetitionsAlpineSki.Discipline.Id == 1 || r.CompetitionsAlpineSki.Discipline.Id == 2))
               .Select(r => new ResultCompetitionAlpineSkiModel
               {
                   CompetitionsAlpineSki = new CompetitionAlpineSkiModel
                   {
                       CompetitionType = r.CompetitionsAlpineSki.CompetitionType,
                       DateTime = r.CompetitionsAlpineSki.DateTime,
                       Discipline = r.CompetitionsAlpineSki.Discipline,
                       Id = r.CompetitionsAlpineSki.Id,
                       PlaceAlpineSki = r.CompetitionsAlpineSki.PlaceAlpineSki,
                       Season = r.CompetitionsAlpineSki.Season,
                       Tournament = r.CompetitionsAlpineSki.Tournament,

                   },

                   Id = r.Id,
                   Place = r.Place,
                   Skier = new SkierModel
                   {
                       Name = r.Skier.Name,
                       Id = r.Skier.Id,
                       Country = new CountryModel
                       {
                           Name = r.Skier.Country.Name,
                           PicturePath = r.Skier.Country.LargePicturePath,
                           ShortName = r.Skier.Country.ShortName
                       },
                       PicturePath = r.Skier.LargePicturePath
                   },
                   Stage = r.Stage
               });


        public IEnumerable<ResultCompetitionAlpineSkiModel> AllBySkierByPlaceByTournamentByDiscipline(int skierId, int place, int tournamentId, string disciplineName)
          => this.db
               .ResultsAlpineSki
               .Where(r => r.SkierId == skierId && r.Place == place && r.CompetitionsAlpineSki.TournamentId == tournamentId && r.CompetitionsAlpineSki.Discipline.Name == disciplineName
                && (r.Stage == Stage || r.CompetitionsAlpineSki.Discipline.Id == 1 || r.CompetitionsAlpineSki.Discipline.Id == 2))
               .Select(r => new ResultCompetitionAlpineSkiModel
               {
                   CompetitionsAlpineSki = new CompetitionAlpineSkiModel
                   {
                       CompetitionType = r.CompetitionsAlpineSki.CompetitionType,
                       DateTime = r.CompetitionsAlpineSki.DateTime,
                       Discipline = r.CompetitionsAlpineSki.Discipline,
                       Id = r.CompetitionsAlpineSki.Id,
                       PlaceAlpineSki = r.CompetitionsAlpineSki.PlaceAlpineSki,
                       Season = r.CompetitionsAlpineSki.Season,
                       Tournament = r.CompetitionsAlpineSki.Tournament
                   },

                   Id = r.Id,
                   Place = r.Place,
                   Skier = new SkierModel
                   {
                       Name = r.Skier.Name,
                       Id = r.Skier.Id,
                       Country = new CountryModel
                       {
                           Name = r.Skier.Country.Name,
                           PicturePath = r.Skier.Country.LargePicturePath,
                           ShortName = r.Skier.Country.ShortName
                       },
                       PicturePath = r.Skier.LargePicturePath
                   },
                   Stage = r.Stage
               });

    }
}
