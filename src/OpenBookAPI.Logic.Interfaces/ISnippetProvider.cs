using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface ISnippetProvider
    {
        /// <summary>
        /// Gets the current snippets  
        /// </summary>
        /// <returns>an <see cref="IEnumerable{T}"/> of <see cref="Snippet"/></returns>
        Task<IEnumerable<Snippet>> GetSnippets();

        /// <summary>
        /// Gets a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id">the <see cref="Snippet"/> Id</param>
        /// <returns>an <see cref="Snippet"/></returns>
        Task<Snippet> GetSnippet(Guid id);

        /// <summary>
        /// Gets snippets for a story
        /// </summary>
        /// <returns>an <see cref="IEnumerable{T}"/> of <see cref="Snippet"/></returns>
        Task<IEnumerable<Snippet>> GetSnippetsForStory(Guid storyId);

        /// <summary>
        /// Get all the chosen snippets for the story
        /// </summary>
        /// <param name="storyId">Story Id</param>
        /// <returns>an <see cref="IEnumerable{T}"/> of <see cref="Snippet"/></returns>
        Task<IEnumerable<Snippet>> GetChosenSnippetsForStory(Guid storyId);

        /// <summary>
        /// Attempts to create a <see cref="Snippet"/>
        /// </summary>
        /// <param name="snippet">Submitted <see cref="Snippet"/></param>
        /// <returns><see cref="Snippet"/></returns>
        Task<Snippet> SubmitSnippet(Snippet snippet);

        /// <summary>
        /// Attempts to update a <see cref="Snippet"/>
        /// </summary>
        /// <param name="snippet">Editted <see cref="Snippet"/></param>
        /// <returns><see cref="Snippet"/></returns>
        Task<Snippet> UpdateSnippet(Snippet snippet);

        /// <summary>
        /// Attempts to delete a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id"><see cref="Guid"/> of snippet to delete</param>
        /// <returns><see cref="bool"/> success</returns>
        Task<bool> DeleteSnippet(Guid id);

        /// <summary>
        /// Flag snippet as inappropriate TODO: Refactor this out of the snippetProvider
        /// </summary>
        /// <param name="snippetId"></param>
        Task<Snippet> FlagSnippet(Guid snippetId);

        /// <summary>
        /// Get snippets for a particular submission period
        /// </summary>
        /// <param name="submissionPeriodId"><see cref="Guid"/> of the submission period</param>
        /// <returns>an <see cref="IEnumerable{T}"/> of <see cref="Snippet"/></returns>
        Task<IEnumerable<Snippet>> GetSnippetsForSubmissionPeriod(Guid submissionPeriodId);
    }
}
