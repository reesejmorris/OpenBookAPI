using Microsoft.Framework.DependencyInjection;

namespace OpenBookAPI
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public static class Modules
    {
        public static void Register(this IServiceCollection services)
        {
            /**********************  Logic  ***********************/
            services.AddScoped<OpenBookAPI.Logic.Interfaces.ISnippetProvider, OpenBookAPI.Logic.SnippetProvider>();
            services.AddScoped<OpenBookAPI.Logic.Interfaces.IStoryProvider, OpenBookAPI.Logic.StoryProvider>();
            services.AddScoped<OpenBookAPI.Logic.Interfaces.ISubmissionPeriodProvider, OpenBookAPI.Logic.SubmissionPeriodProvider>();
            services.AddScoped<OpenBookAPI.Logic.Interfaces.IVoteProvider, OpenBookAPI.Logic.VoteProvider>();
            services.AddScoped<OpenBookAPI.Logic.Interfaces.IFlagProvider, OpenBookAPI.Logic.FlagProvider>();

            /**********************  Data   ***********************/
            //In memory Data
            services.AddInstance(typeof(OpenBookAPI.Data.Interfaces.ISnippetRepository), new OpenBookAPI.Data.InMemory.SnippetRepository());
            services.AddInstance(typeof(OpenBookAPI.Data.Interfaces.IStoryRepository), new OpenBookAPI.Data.InMemory.StoryRepository());
            services.AddInstance(typeof(OpenBookAPI.Data.Interfaces.ISubmissionPeriodRepository), new OpenBookAPI.Data.InMemory.SubmissionPeriodRepository());
            services.AddInstance(typeof(OpenBookAPI.Data.Interfaces.IVoteRepository), new OpenBookAPI.Data.InMemory.VoteRepository());
            services.AddInstance(typeof(OpenBookAPI.Data.Interfaces.IFlagRepository), new OpenBookAPI.Data.InMemory.FlagRepository());
        }
    }
}
