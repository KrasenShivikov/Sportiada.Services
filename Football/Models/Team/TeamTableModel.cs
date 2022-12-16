namespace Sportiada.Services.Football.Models.Team
{
    public class TeamTableModel
    {
        public string TeamName { get; set; }

        public int Rank { get; set; }

        public int OverallPoints { get; set; }

        public int OverallMathes { get; set; }

        public int OverallScoredGoals { get; set; }

        public int OverallAllowedGoals { get; set; }

        public int OverallWins { get; set; }

        public int OverallLoses { get; set; }

        public int OverallDraws { get; set; }

        public int HostPoints { get; set; }

        public int HostMathes { get; set; }

        public int HostScoredGoals { get; set; }

        public int HostAllowedGoals { get; set; }

        public int HostWins { get; set; }

        public int HostLoses { get; set; }

        public int HostDraws { get; set; }

        public int GuestPoints { get; set; }

        public int GuestMathes { get; set; }

        public int GuestScoredGoals { get; set; }

        public int GuestAllowedGoals { get; set; }

        public int GuestWins { get; set; }

        public int GuestLoses { get; set; }

        public int GuestDraws { get; set; }

        public int OveralGoalDiff => OverallScoredGoals - OverallAllowedGoals;
    }
}
