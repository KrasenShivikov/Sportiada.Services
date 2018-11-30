namespace Sportiada.Services.Football.Models.PlayerIn
{
    public class PlayerInBaseModel
    {
        public int PlayerId { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public string InIcon { get; set; }
    }
}
