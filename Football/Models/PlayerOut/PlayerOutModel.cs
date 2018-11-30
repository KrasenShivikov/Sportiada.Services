namespace Sportiada.Services.Football.Models.PlayerOut
{
    using Models.Player;

    public class PlayerOutModel
    {
        public int Id { get; set; }

        public PlayerModel Player { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public string OutIcon { get; set; }
    }
}
