namespace Sportiada.Services.Football.Models.GoalAssistance
{
    using Models.Player;

    public class GoalAssistanceModel
    {
        public int Id { get; set; }

        public PlayerModel Player { get; set; }
    }
}
