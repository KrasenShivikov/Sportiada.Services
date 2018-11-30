namespace Sportiada.Services.Football.Models.Goal
{
    using Models.GoalAssistance;

    public class GoalBaseModel
    {
        public int PlayerId { get; set; }

        public GoalAssistanceBaseModel Assistance { get; set; }

        public int TypeId { get; set; }

    }
}
