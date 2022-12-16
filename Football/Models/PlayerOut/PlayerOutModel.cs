namespace Sportiada.Services.Football.Models.PlayerOut
{
    using Models.Player;

    public class PlayerOutModel
    {
        public int Id { get; set; }

        public PlayerModel Player { get; set; }

        public string OutIcon => @"~/images/FootballGameIcons/PlayerOut.png";
    }
}
