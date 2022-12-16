namespace Sportiada.Services.Football.Models.GameStatitistic
{
    using Models.Card;
    using Models.Goal;
    using Models.LineUp;
    using Models.Reserve;
    using Models.Squad;
    using Models.Sideline;
    using Models.Substitute;
    using System.Collections.Generic;

    public class GameTeamStatitisticFullModel : GameTeamStatisticModel
    {
        public SquadGameModel Squad { get; set; }

        public List<LineUpModel> LineUps { get; set; }

        public List<SidelineModel> Sidelines { get; set; }

        public List<SubstituteModel> Substitutes { get; set; }

        public List<ResModel> Reserves { get; set; }

        public List<CardModel> Cards { get; set; }

        public List<GoalModel> Goals { get; set; }

        public int BallPossession { get; set; }

        public int Corners { get; set; }

        public int ShotsOnTarget { get; set; }

        public int ShotsWide { get; set; }

        public int Fouls { get; set; }

        public int Offsides { get; set; }
    }
}
