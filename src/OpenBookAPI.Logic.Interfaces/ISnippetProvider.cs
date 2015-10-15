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
        IEnumerable<Snippet> GetSnippets();

        /// <summary>
        /// Gets a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id">the <see cref="Snippet"/> Id</param>
        /// <returns>an <see cref="Snippet"/>></returns>
        Snippet GetSnippet(Guid id);

        /// <summary>
        /// Attempts to create a <see cref="Snippet"/>
        /// </summary>
        /// <param name="snippet">Submitted <see cref="Snippet"/></param>
        /// <returns></returns>
        Snippet SubmitSnippet(Snippet snippet);

        /// <summary>
        /// Attempts to update a <see cref="Snippet"/>
        /// </summary>
        /// <param name="snippet">Editted <see cref="Snippet"/></param>
        /// <returns></returns>
        Snippet UpdateSnippet(Snippet snippet);

        /// <summary>
        /// Attempts to delete a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id"><see cref="Guid"/> of snippet to delete</param>
        /// <returns></returns>
        bool DeleteSnippet(Guid id);
    }
}
