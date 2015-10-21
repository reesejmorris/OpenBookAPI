using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Logic;
using OpenBookAPI.Logic.Interfaces;

namespace OpenBookAPI
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public static class Modules
    {
        public static void Register(this IServiceCollection services)
        {
            /**********************  Logic  ***********************/
            services.AddTransient<ISnippetProvider, SnippetProvider>();
            services.AddTransient<IStoryProvider, StoryProvider>();
            services.AddTransient<ISubmissionPeriodProvider, SubmissionPeriodProvider>();
            services.AddTransient<IVoteProvider, VoteProvider>();

            /**********************  Data   ***********************/
            //In memory Data
            services.AddInstance(typeof(ISnippetRepository), new OpenBookAPI.Data.InMemory.SnippetRepository());
            services.AddInstance(typeof(IStoryRepository), new OpenBookAPI.Data.InMemory.StoryRepository());
            services.AddInstance(typeof(ISubmissionPeriodRepository), new OpenBookAPI.Data.InMemory.SubmissionPeriodRepository());
            services.AddInstance(typeof(IVoteRepository), new OpenBookAPI.Data.InMemory.VoteRepository());
        }
    }
}
