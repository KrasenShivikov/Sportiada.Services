
namespace Sportiada.Services.Football.Models.Goal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GoalsByPlayerModel
    {
        public string PlayerName { get; set; }

        public IDictionary<string, int> GoalsByTournament { get; set; } = new Dictionary<string, int>();

        public int Count => GetGoalsCount();

        private int GetGoalsCount()
        {
            int count = 0;

            foreach (var g in GoalsByTournament)
            {
                count += g.Value;
            }

            return count;
        }
    }
}
