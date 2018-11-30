namespace Sportiada.Services.Football.Models.Goal
{
    using Models.Player;

    public class GoalTeamSeasonStatisticModel
    {
        public int Id { get; set; }

        public PlayerTeamSeasonStatisticModel Scorer { get; set; }
    }
}
