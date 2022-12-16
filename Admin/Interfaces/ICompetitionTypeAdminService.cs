using Sportiada.Data.Models;
using Sportiada.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Interfaces
{
    public interface ICompetitionTypeAdminService
    {
        IEnumerable<CompetitionTypeAdminModel> All();

        void Create(string name);

        void Update(int id, string name);

        void Delete(int id);
    }
}
