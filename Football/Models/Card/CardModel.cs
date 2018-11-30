namespace Sportiada.Services.Football.Models.Card
{
    using Models.CardType;
    using Models.Player;

    public class CardModel
    {
        public int Id { get; set; }

        public CardTypeModel Type { get; set; }

        public PlayerModel Player { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

    }
}
