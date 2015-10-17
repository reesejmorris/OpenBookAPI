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
        private List<Snippet> _dataContext;

        public SnippetRepository()
        {
            _dataContext = new List<Snippet>
            {
                new Snippet{
                    Id = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    Content = "Once upon a time there was a little boy named Reese,",
                    Author = "John",
                    NewParagraph = false,
                    StoryId = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    SubmissionDate = new DateTime(2015,10,11),
                    UpVotes = 10,
                    DownVotes = 1,
                    SubmissionPeriodId = new Guid("3f6945fe-83e4-478b-8dd4-9ffbc66a9f35")
                },new Snippet{
                    Id = new Guid("5d6932f8-5886-4723-8eb8-c243c40da684"),
                    Content = "He was always late for his lunch appointments.",
                    Author = "John",
                    NewParagraph = false,
                    StoryId = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    SubmissionDate = new DateTime(2015,10,11,6,0,0),
                    UpVotes = 5,
                    DownVotes = 0,
                    SubmissionPeriodId = new Guid("3f6945fe-83e4-478b-8dd4-9ffbc66a9f35")
                },new Snippet{
                    Id = new Guid("1227e500-071c-48ef-b92d-690a99d0ec21"),
                    Content = "BLAH BLAH BLAH.",
                    Author = "John",
                    NewParagraph = true,
                    StoryId = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    SubmissionDate = new DateTime(2015,10,11,12,0,0),
                    UpVotes = 0,
                    DownVotes = 3,
                    SubmissionPeriodId = new Guid("3f6945fe-83e4-478b-8dd4-9ffbc66a9f35")
                }

            };
        }

        public Snippet GetById(Guid snippetId)
        {
            return _dataContext.FirstOrDefault(s => s.Id == snippetId);
        }

        public IEnumerable<Snippet> GetAll()
        {
            return _dataContext;
        }
        public IEnumerable<Snippet> GetByStory(Guid storyId)
        {
            return _dataContext.Where(a=>a.StoryId == storyId);
        }

        public IEnumerable<Snippet> GetBySubmissionPeriodId(Guid submissionPeriodId)
        {
            return _dataContext.Where(sp => sp.SubmissionPeriodId == submissionPeriodId);
        }

        public Snippet Create(Snippet snippet)
        {
            snippet.Id = new Guid();
            _dataContext.Add(snippet);
            return snippet;
        }

        public Snippet Update(Snippet snippet)
        {
            var old = GetById(snippet.Id);
            if (old == null)
                return null;
            (_dataContext as List<Snippet>).Remove(old);
            (_dataContext as List<Snippet>).Add(snippet);
            return snippet;
        }

        public bool Delete(Guid id)
        {
            var old = GetById(id);
            if (old == null)
                return false;
            return (_dataContext as List<Snippet>).Remove(old);
        }
    }
}
