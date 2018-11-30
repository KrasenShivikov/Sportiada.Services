namespace Sportiada.Services.Football.Models.Card
{
    using Models.CardType;
    using Models.Player;

    public class CardTeamSeasonStatisticModel
    {
        public int Id { get; set; }

        public CardTypeModel Type { get; set; }

        public PlayerTeamSeasonStatisticModel Player { get; set; }
    }
}
