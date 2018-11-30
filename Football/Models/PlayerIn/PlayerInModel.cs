namespace Sportiada.Services.Football.Models.PlayerIn
{
    using Models.Player;

    public class PlayerInModel
    {
        public int Id { get; set; }

        public PlayerModel Player { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public string InIcon { get; set; }
    }
}
