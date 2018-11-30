namespace Sportiada.Services.Football.Models.Side
{
    using Models.Coach;
    using Models.Team;

    public class SideGameModel : SideModel
    {
        public TeamGameModel Team { get; set; }

        public CoachGameModel Coach { get; set; }
    }
}
