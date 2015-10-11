using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.InMemory
{
    
    public class SnippetRepository:ISnippetRepository
    {
        private IEnumerable<ISnippet> _dataContext;

        public SnippetRepository()
        {
            _dataContext = new[]
            {
                new Snippet{
                    Id = Guid.Empty,
                    Content = "Once upon a time there was a little boy named Reese,",
                    Author = "John",
                    NewParagraph = false,
                    StoryId = Guid.Empty,
                    SubmissionDate = new DateTime(2015,10,11),
                    UpVotes = 10,
                    DownVotes = 1
                },new Snippet{
                    Id = Guid.NewGuid(),
                    Content = "He was always late for his lunch appointments.",
                    Author = "John",
                    NewParagraph = false,
                    StoryId = Guid.Empty,
                    SubmissionDate = new DateTime(2015,10,11,6,0,0),
                    UpVotes = 5,
                    DownVotes = 0
                },new Snippet{
                    Id = Guid.NewGuid(),
                    Content = "BLAH BLAH BLAH.",
                    Author = "John",
                    NewParagraph = true,
                    StoryId = Guid.Empty,
                    SubmissionDate = new DateTime(2015,10,11,12,0,0),
                    UpVotes = 0,
                    DownVotes = 3
                }

            }.AsEnumerable<ISnippet>();
        }

        public ISnippet GetById(Guid id)
        {
            return _dataContext.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<ISnippet> GetByStory(Guid Id)
        {
            return _dataContext.Where(a=>a.StoryId == Id);
        }

        public IEnumerable<ISnippet> GetSubmissionsByStory(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
