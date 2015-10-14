using System;

namespace OpenBookAPI.Models
{
    public class Snippet
    {
        public virtual Guid StoryId { get; set; }
        public virtual Guid SubmissionPeriodId { get; set; }
        public virtual Guid Id { get; set; }
        public virtual DateTime SubmissionDate { get; set; }
        public virtual bool NewParagraph { get; set; }
        public virtual string Content { get; set; }
        public virtual string Author { get; set; }
        public virtual int UpVotes { get; set; }
        public virtual int DownVotes { get; set; }
    }
}
