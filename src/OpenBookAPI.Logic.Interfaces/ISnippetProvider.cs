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
        /// <param name="id">the snippet Id</param>
        /// <returns>an <see cref="Snippet"/>></returns>
        Snippet GetSnippet(Guid id);
    }
}
