using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface IStoryProvider
    {
        IEnumerable<Story> GetStories();
        Story GetStory(Guid id);
        Story GetLatest();
    }
}