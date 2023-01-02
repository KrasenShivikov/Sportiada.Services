namespace Sportiada.Services.Football.Models.GoalAssistance
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class GoalAssistanceByPlayerModel
    {
        public string PlayerName { get; set; }

        public IDictionary<string, int> GoalAssistancesByTournament { get; set; } = new Dictionary<string, int>();

        public int Count => GetGoalAssistansesCount();

        private int GetGoalAssistansesCount()
        {
            int count = 0;

            foreach (var g in GoalAssistancesByTournament)
            {
                count += g.Value;
            }

            return count;
        }
    }
}
