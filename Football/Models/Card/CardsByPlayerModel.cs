namespace Sportiada.Services.Football.Models.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CardsByPlayerModel
    {
        public string PlayerName { get; set; }

        public IDictionary<string, int> CardsByTournament { get; set; }
    }
}
