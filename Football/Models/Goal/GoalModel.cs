namespace Sportiada.Services.Football.Models.Goal
{
    using Models.GoalAssistance;
    using Models.GoalType;
    using Models.Player;
   
    public class GoalModel
    {
        public int Id { get; set; }

        public GoalTypeModel Type { get; set; }

        public PlayerModel Player { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public GoalAssistanceModel Assistance { get; set; }
    }
}
