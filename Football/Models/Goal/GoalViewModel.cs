namespace Sportiada.Services.Football.Models.Goal
{
    public class GoalViewModel
    {
        public int ScorerId { get; set; }

        public string ScorerName { get; set; }

        public int? GoalAsistantId { get; set; }

        public string GoalAsistantName { get; set; }

        public string Minute { get; set; }

        public bool FirstHalf { get; set; }

        public bool HostGoal { get; set; }

        public int CountHostGoals { get; set; }

        public int CountGuestGoals { get; set; }
    }
}
