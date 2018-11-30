namespace Sportiada.Services.Football.Models.PlayerOut
{
    public class PlayerOutBaseModel
    {
        public int PlayerId { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public string OutIcon { get; set; }
    }
}
