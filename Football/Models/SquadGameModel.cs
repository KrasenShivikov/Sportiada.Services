namespace Sportiada.Services.Football.Models.Squad
{
    using Models.Coach;
    using Models.Team;

    public class SquadGameModel : SquadModel
    {
        public TeamGameModel Team { get; set; }

        public CoachGameModel Coach { get; set; }
    }
}
