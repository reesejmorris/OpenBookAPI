using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface ISnippet
    {
        Guid StoryId { get; set; }
        Guid Id { get; set; }
        DateTime SubmissionDate { get; set; }
        int DownVotes { get; set; }
        int UpVotes { get; set; }
        bool NewParagraph { get; set; }
        string Content { get; set; }
        string Author { get; set; }
    }
}
