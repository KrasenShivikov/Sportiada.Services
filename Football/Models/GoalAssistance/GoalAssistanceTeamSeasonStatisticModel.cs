namespace Sportiada.Services.Football.Models.GoalAssistance
{
    using Models.Player;

    public class GoalAssistanceTeamSeasonStatisticModel
    {
        public int Id { get; set; }

        public PlayerTeamSeasonStatisticModel GoalAssistant { get; set; }
    }
}
