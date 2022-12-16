namespace Sportiada.Services.Football.Models.Substitute
{
    using Models.PlayerIn;
    using Models.PlayerOut;

    public class SubstituteModel
    {
        public int Id { get; set; }

        public string Minute { get; set; }

        public bool FirstHalf { get; set; }

        public PlayerInModel PlayerIn { get; set; }

        public PlayerOutModel PlayerOut { get; set; }
    }
}
