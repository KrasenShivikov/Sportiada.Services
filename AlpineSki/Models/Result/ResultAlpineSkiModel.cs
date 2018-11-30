namespace Sportiada.Services.AlpineSki.Models.Result
{
    using Intermediate;
    using Models.Skier;
    using System.Collections.Generic;

    public class ResultAlpineSkiModel
    { 
        public int Id { get; set; }

        public SkierModel Skier { get; set; }

        public string Stage { get; set; }

        public string TimeFirstManch { get; set; }

        public string TimeSecondManch { get; set; }

        public string FinalTime { get; set; }

        public string Difference { get; set; }

        public string Metters { get; set; }

        public int Place { get; set; }

        public int Bib { get; set; }

        public int StartOrder { get; set; }

        public List<IntermediateAlpineSkiModel> Intermediates { get; set; }
    }
}
