using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenBookAPI.Models;

namespace OpenBookAPI.ViewModels
{
    public class StorySoFar
    {
        public Story Story { get; set; }
        public IEnumerable<Snippet> ChosenSnippets { get; set; }
        public SubmissionPeriod CurrentSubmissionPeriod { get; set; }
        public IEnumerable<Snippet> SubmittedSnippets { get; set; }
    }
}
