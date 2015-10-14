using System;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.InMemory
{
    public interface IStoryRepository
    {
        Story GetById(Guid id);
    }
}