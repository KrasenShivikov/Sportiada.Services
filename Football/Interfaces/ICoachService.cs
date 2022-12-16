namespace Sportiada.Services.Football.Interfaces
{
    using Sportiada.Services.Football.Models.Coach;
    using Sportiada.Services.Football.Models.SquadCoach;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface ICoachService
    {
        IEnumerable<SquadCoachModel> CoachesBySquad(int squadId);
    }

}
