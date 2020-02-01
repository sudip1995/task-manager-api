using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskManager.Business.GraphQL;
using TaskManager.Business.Services;
using TaskManager.Contracts.Models;
using TaskManager.GraphQL;

namespace TaskManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<TaskManagerStoreDatabaseSettings>(
                Configuration.GetSection(nameof(TaskManagerStoreDatabaseSettings)));

            services.AddSingleton<ITaskManagerStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TaskManagerStoreDatabaseSettings>>().Value);

            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<IColumnService, ColumnService>();
            services.AddTransient<ITicketService, TicketService>();

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<TaskManagerQuery>();
            services.AddSingleton<TaskManagerMutation>();

            services.AddSingleton<BoardGraphType>();
            services.AddSingleton<BoardInputGraphType>();
            services.AddSingleton<ColumnGraphType>();
            services.AddSingleton<ColumnInputGraphType>();
            services.AddSingleton<TicketGraphType>();
            services.AddSingleton<TicketInputGraphType>();

            services.AddTransient<ITaskManagerDataProvider, TaskManagerDataProvider>();
            services.AddTransient<ITaskManagerDataMutator, TaskManagerDataMutator>();

            var serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new TaskManagerSchema(new FuncDependencyResolver(type => serviceProvider.GetService(type))));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseGraphiQl("/graphql");
            app.UseMvc();
        }
    }
}
