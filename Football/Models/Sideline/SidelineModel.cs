namespace Sportiada.Services.Football.Models.Sideline
{
    using Models.Player;
    using Models.SidelineReason;

    public class SidelineModel
    {
        public int Id { get; set; }

        public PlayerModel Player { get; set; }

        public SidelineReasonModel Reason { get; set; }
    }
}
