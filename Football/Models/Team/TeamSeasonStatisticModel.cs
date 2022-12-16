namespace Sportiada.Services.Football.Models.Team
{
    using Models.Card;
    using Models.Coach;
    using Models.Game;
    using Models.Goal;
    using Models.GoalAssistance;
    using Models.Squad;
    using System.Collections.Generic;

    public class TeamSeasonStatisticModel
    {
        public SquadGameListModel Side { get; set; }

        public List<GoalTeamSeasonStatisticModel> Goals { get; set; }

        public List<GoalAssistanceTeamSeasonStatisticModel> GoalAssistances { get; set; }

        public List<CardTeamSeasonStatisticModel> Cards { get; set; }

        public List<PenaltyTeamSeasonStattisticModel> Penalties { get; set; }

        public List<GameModel> Games { get; set; }

        public List<CoachSideSeasonModel> Coaches { get; set; }

        Dictionary<string, int> Scorers { get; set; }

        Dictionary<string, int> ScoreAssistances { get; set; }

        Dictionary<string, int> YellowCards { get; set; }

        Dictionary<string, int> RedCards { get; set; }

        Dictionary<string, int> OwnGoals { get; set; }


    }
}
