namespace Sportiada.Services.Football.Models.Coach
{
    using Services.Models;

    public class CoachGameModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryFootballGameModel Country { get; set; }
    }
}
