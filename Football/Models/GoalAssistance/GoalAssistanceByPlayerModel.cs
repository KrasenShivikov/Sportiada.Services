namespace Sportiada.Services.Football.Models.GoalAssistance
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class GoalAssistanceByPlayerModel
    {
        public string PlayerName { get; set; }

        public IDictionary<string, int> GoalAssistancesByTournament { get; set; }
    }
}
