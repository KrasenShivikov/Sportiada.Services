namespace Sportiada.Services.Football.Models.Player
{
    using Services.Models;

    public class PlayerModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PlayerNumber { get; set; }

        public CountryFootballGameModel Country { get; set; }
    }
}
