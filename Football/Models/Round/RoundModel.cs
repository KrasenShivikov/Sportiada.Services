namespace Sportiada.Services.Football.Models.Round
{
    using Models.Competitiion;

    public class RoundModel : RoundBaseModel
    {
        public CompetitionModel Competition { get; set; }
    }
}
