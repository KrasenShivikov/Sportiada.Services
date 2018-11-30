namespace Sportiada.Services.Football.Models.Referee
{
    using Services.Models;

    public class RefereeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryFootballGameModel Country { get; set; }
    }
}
