namespace Sportiada.Services.AlpineSki.Models.Competition
{
    using System.Collections.Generic;
    using Result;


    public class CompetitionAlpineSkiWithResultsModel : CompetitionAlpineSkiModel
    {
        public string Track { get; set; }

        public string FisTechnicalDelegate { get; set; }

        public string Referee { get; set; }

        public string AssistantRef { get; set; }

        public string CompetitionAlpineSkiChief { get; set; }

        public string StartRef { get; set; }

        public string FinishRef { get; set; }

        public List<ResultAlpineSkiModel> Results { get; set; } 
    }
}
