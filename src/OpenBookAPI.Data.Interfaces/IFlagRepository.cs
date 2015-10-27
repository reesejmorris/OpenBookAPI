using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.Interfaces
{
    public interface IFlagRepository
    {
        Flag CreateFlag(Flag newFlag);
        bool DeleteFlag(Guid flagId);
        IEnumerable<Flag> GetAll();
        Flag GetById(Guid flagId);
    }
}