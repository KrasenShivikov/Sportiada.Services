namespace Sportiada.Services.AlpineSki.Models.Result
{
    using Models.Competition;

    public class ResultSkierStatisticAlpineSkiModel
    {
        public int Id { get; set; }

        public string Stage { get; set; }

        public CompetitionAlpineSkiModel CompetitionsAlpineSki { get; set; }

        public int Place { get; set; }

    }
}
