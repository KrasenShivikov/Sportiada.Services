namespace Sportiada.Services.Football.Interfaces
{
    using Models.Tournament;
    using System.Collections.Generic;

    public interface ITournamentService
    {
        IEnumerable<TournamentModel> All();
    }
}
