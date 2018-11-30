namespace Sportiada.Services.Football.Models.Player
{
    using Models.Substitute;

    public class PlayerGameStatisticModel
    {
        public int GameId { get; set; }

        public string SeasonName { get; set; }

        public int PlayerId { get; set; }

        public string PlayerTeam { get; set; }

        public string RivalTeam { get; set; }

        public bool InLineUp { get; set; }

        public bool IsReserve { get; set; }

        public SubstituteInModel SubstituteIn { get; set; }

        public SubstituteOutModel substituteOut { get; set; }

        public bool Sideline { get; set; }

        public int YellowCards { get; set; }

        public int RedCard { get; set; }

        public int ScoredGoals { get; set; }

        public int OwnGoals { get; set; }

        public int GoalAssistances { get; set; }

        public int AllowedGaols { get; set; }

        public int ScoredPenalties { get; set; }

        public int SavedPenalties => 0;
    }
}
