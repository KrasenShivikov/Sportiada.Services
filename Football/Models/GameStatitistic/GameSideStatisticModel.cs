namespace Sportiada.Services.Football.Models.GameStatitistic
{
    using Models.Card;
    using Models.Goal;
    using Models.LineUp;
    using Models.Reserve;
    using Models.Substitute;
    using Models.Team;
    using Models.Season;
    using System.Collections.Generic;

    public class GameSideStatisticModel
    {
        public int GameId { get; set; }

        public int SideId { get; set; }

        public SeasonModel Season { get; set; }

        public string Team { get; set; }

        public TeamModel RivalTeam { get; set; }

        public List<RivalGoalModel> RivalTeamGoals { get; set; }

        public List<LineUpBaseModel> LineUps { get; set; }

        public List<SubstituteBaseModel> Substitutes { get; set; }

        public List<ResBaseModel> Reserves { get; set; }

        public List<CardBaseModel> Cards { get; set; }

        public List<GoalBaseModel> Goals { get; set; }
    }
}
