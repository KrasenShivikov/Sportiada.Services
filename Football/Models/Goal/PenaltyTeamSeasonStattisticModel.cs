namespace Sportiada.Services.Football.Models.Goal
{
    using Models.GoalType;
    using Models.Round;

    public class PenaltyTeamSeasonStattisticModel
    {
        public int Id { get; set; }

        public GoalTypeModel Penalties { get; set; }

        public RoundModel Round { get; set; }
    }
}
