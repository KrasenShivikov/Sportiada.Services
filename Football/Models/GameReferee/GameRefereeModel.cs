namespace Sportiada.Services.Football.Models.GameReferee
{
    using Models.Referee;
    using Models.RefereeType;

    public class GameRefereeModel
    {
        public int Id { get; set; }

        public RefereeModel Referee { get; set; }

        public RefereeTypeModel Type { get; set; }
    }
}
