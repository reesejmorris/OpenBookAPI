using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Models
{
    public class Story
    {
        /// <summary>
        /// The Story Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Title of the Story
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The date the story began
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Status of the story
        /// </summary>
        public StoryStatus Status { get; set; }
    }
    public enum StoryStatus
    {
        Draft,
        Open,
        Closed,
        Deleted
    }
}
