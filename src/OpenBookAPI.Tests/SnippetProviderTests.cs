using OpenBookAPI.Data.InMemory;
using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Logic;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OpenBookAPI.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class SnippetProviderTests
    {
        ISnippetRepository _repo = new SnippetRepository();
        ISnippetProvider _provider;
        public SnippetProviderTests()
        {
            _provider = new SnippetProvider(_repo);
        }
        [Fact]
        public void GetSnippetWithEmptyGuidShouldReturnNull()
        {
            Assert.Null(_provider.GetSnippet(Guid.Empty));
        }
        [Fact]
        public void GetSnippetShouldReturnSnippet()
        {
            Assert.IsType<Snippet>(_provider.GetSnippet(new Guid("1227e500-071c-48ef-b92d-690a99d0ec21")));
        }
    }
}
