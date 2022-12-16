namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballGameStatisticAdminModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public FootballGameStatisticTypeAdminModel Type { get; set; }

        public int SquadId { get; set; }

        public FootballSquadAdminModel Squad { get; set; }

        public int StadiumId { get; set; }

        public FootballStadiumAdminModel Stadium { get; set; }

        public IEnumerable<FootballGameStatisticCoachAdminModel> Coaches { get; set; }

        public IEnumerable<FootballLineUpAdminModel> LineUps { get; set; }

        public IEnumerable<FootballSidelineAdminModel> Sidelines { get; set; }

        public IEnumerable<FootballReserveAdminModel> Reserves { get; set; }

        public IEnumerable<FootballSubstituteAdminModel> Substitutes { get; set; }

        public IEnumerable<FootballCardAdminModel> Cards { get; set; }

        public IEnumerable<FootballGoalAdminModel> Goals { get; set; }

        public int BallPossession { get; set; }

        public int Corners { get; set; }

        public int ShotsOnTarget { get; set; }

        public int ShotsWide { get; set; }

        public int Fouls { get; set; }

        public int Offsides { get; set; }
    }
}
