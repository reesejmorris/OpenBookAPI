using System;

namespace OpenBookAPI.Models
{
    public class Snippet
    {
        public Guid StoryId { get; set; }
        public Guid SubmissionPeriodId { get; set; }
        public Guid Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public bool NewParagraph { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int Score { get; set; }
        public int Flags { get; set; }
        public SnippetStatus Status { get; set; }
    }

    public enum SnippetStatus
    {
        Submitted = 0,
        Chosen = 1,
        Rejected = 2,
    }
}
