
namespace Sportiada.Services.Football.Models.Goal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GoalsByPlayerModel
    {
        public string PlayerName { get; set; }

        public IDictionary<string, int> GoalsByTournament { get; set; }
    }
}
