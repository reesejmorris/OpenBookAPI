using System;
using OpenBookAPI.Models;
using System.Collections.Generic;

namespace OpenBookAPI.Data.Interfaces
{
    public interface IStoryRepository
    {
        Story GetById(Guid id);
        IEnumerable<Story> GetAll();
    }
}