﻿namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballSidelineReasonAdminService
    {
        IEnumerable<FootballSidelineReasonAdminModel> All();

        void Create(string name, string picture);

        void Update(int id, string name, string picture);

        void Delete(int id);

        FootballSidelineReason GetReason(int id);

    }
}
