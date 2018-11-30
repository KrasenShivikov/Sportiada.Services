namespace Sportiada.Services.Football.Models.Substitute
{
    using Models.PlayerIn;
    using Models.PlayerOut;

    public class SubstituteBaseModel
    {
        public PlayerInBaseModel PlayerIn { get; set; }

        public PlayerOutBaseModel PlayerOut { get; set; }
    }
}
