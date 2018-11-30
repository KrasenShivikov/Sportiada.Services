namespace Sportiada.Services.Football.Models.Game
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class GameSearchModel
    {
        public List<SelectListItem> Tournaments { get; set; }

        public List<SelectListItem> Seasons { get; set; }
    }
}
