namespace Sportiada.Services.Football.Models.PlayerIn
{
    using Models.Player;

    public class PlayerInModel
    {
        public int Id { get; set; }

        public PlayerModel Player { get; set; }

        public string InIcon => @"~/images/FootballGameIcons/PlayerIn.png";
    }
}
