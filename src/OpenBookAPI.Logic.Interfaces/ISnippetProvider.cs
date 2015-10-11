using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface ISnippetProvider
    {
        /// <summary>
        /// Gets the current snippets for the latest story 
        /// </summary>
        /// <returns>an <see cref="IEnumerable{T}"/> of <see cref="ISnippet"/></returns>
        IEnumerable<ISnippet> GetStorySoFar();

        /// <summary>
        /// Gets a <see cref="ISnippet"/>
        /// </summary>
        /// <param name="id">the snippet Id</param>
        /// <returns>an <see cref="ISnippet"/>></returns>
        ISnippet GetSnippet(Guid id);
    }
}
