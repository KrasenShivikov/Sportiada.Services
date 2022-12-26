namespace Sportiada.Services.Football.Models.Player
{
    using Models.Substitute;
    using Models.Card;
    using Models.Goal;
    using Models.GoalAssistance;
    using System.Collections.Generic;

    public class PlayerGameStatisticModel
    {
        public int GameId { get; set; }

        public string SeasonName { get; set; }

        public string TournamentName { get; set; }

        public int PlayerId { get; set; }

        public string PlayerTeam { get; set; }

        public string RivalTeam { get; set; }

        public bool InLineUp { get; set; }

        public bool IsReserve { get; set; }

        public SubstituteInModel SubstituteIn { get; set; }

        public SubstituteOutModel SubstituteOut { get; set; }

        public bool Sideline { get; set; }

        public IEnumerable<string> SidelineReasons { get; set; }

        public CardPlayerStatisticModel YellowCard { get; set; }

        public CardPlayerStatisticModel SecondYellowCard { get; set; }

        public CardPlayerStatisticModel RedCard { get; set; }

        public IEnumerable<GoalPlayerStatisticModel> ScoredGoals { get; set; }

        public IEnumerable<GoalPlayerStatisticModel> OwnGoals { get; set; }

        public IEnumerable<GoalAssistancePlayerStatisticModel> GoalAssistances { get; set; }

        public IEnumerable<GoalPlayerStatisticModel> AllowedGoals { get; set; }

        public IEnumerable<GoalPlayerStatisticModel> ScoredPenalties { get; set; }

        public IEnumerable<GoalPlayerStatisticModel> MissedPenalties { get; set; }

        public IEnumerable<GoalPlayerStatisticModel> SavedPenalties { get; set; }
    }
}
