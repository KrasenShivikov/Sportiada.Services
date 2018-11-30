namespace Sportiada.Services.AlpineSki.Models.Competition
{
    using Data.Models;
    using Data.Models.AlpineSki;
    using System;

    public class CompetitionAlpineSkiModel
    {

        public int Id { get; set; }

        public DateTime DateTime {get;set;}

        public CompetitionType CompetitionType { get; set; }

        public AlpineSkiDisciplines Discipline { get; set; }

        public PlaceAlpineSki PlaceAlpineSki { get; set; }

        public Season Season { get; set; }

        public Tournament Tournament { get; set; }
    }
}
