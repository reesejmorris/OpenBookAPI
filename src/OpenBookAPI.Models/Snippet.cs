using System;
using OpenBookAPI.Logic.Interfaces;

namespace OpenBookAPI.Models
{
    public class Snippet:ISnippet
    {
        private Guid storyId;

        public Guid StoryId
        {
            get { return storyId; }
            set { storyId = value; }
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime submissionDate;

        public DateTime SubmissionDate
        {
            get { return submissionDate; }
            set { submissionDate = value; }
        }

        private bool newParagraph;

        public bool NewParagraph
        {
            get { return newParagraph; }
            set { newParagraph = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private int upVotes;

        public int UpVotes
        {
            get { return upVotes; }
            set { upVotes = value; }
        }

        private int downVotes;
        public int DownVotes
        {
            get
            {
                return downVotes;
            }

            set
            {
                downVotes = value;
            }
        }
    }
}
