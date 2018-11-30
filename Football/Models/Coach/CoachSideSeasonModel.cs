using System;

namespace Sportiada.Services.Football.Models.Coach
{
    public class CoachSideSeasonModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? UntilDate { get; set; }
    }
}
