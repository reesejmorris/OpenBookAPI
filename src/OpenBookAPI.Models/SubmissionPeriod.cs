using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Models
{
    public class SubmissionPeriod
    {
        /// <summary>
        /// Id of this submission period
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the parent Story
        /// </summary>
        public Guid StoryId { get; set; }

        /// <summary>
        /// the position of the chosen snippet in the story
        /// </summary>
        public int Location { get; set; }
    }
}
