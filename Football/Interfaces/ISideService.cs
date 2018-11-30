namespace Sportiada.Services.Football.Interfaces
{
    using Models.Side;

    public interface ISideService
    {
        SideTeamModel ById(int id);
    }
}
