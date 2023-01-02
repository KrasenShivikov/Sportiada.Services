namespace Sportiada.Services.Football.Models.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CardsByPlayerModel
    {
        public string PlayerName { get; set; }

        public IDictionary<string, int> CardsByTournament { get; set; } = new Dictionary<string, int>();

        public int Count => GetCardsCount();

        private int GetCardsCount()
        {
            int count = 0;

            foreach (var g in CardsByTournament)
            {
                count += g.Value;
            }

            return count;
        }
    }
}
