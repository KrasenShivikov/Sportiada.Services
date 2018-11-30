namespace Sportiada.Services.Football.Models.Reserve
{
    using Models.Player;

    public class ResModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public PlayerModel Player { get; set; }
    }
}
