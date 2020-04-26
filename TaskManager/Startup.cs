using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Business.GraphQL;
using TaskManager.GraphQL;
using TaskManager.Library.Helpers;
using TaskManager.Library.Ioc;

namespace TaskManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            ConfigurationHelper.Instance.AttachConfigurationProvider(configuration);
            ConfigurationHelper.Instance.IsDevelopment = environment.IsDevelopment();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            IocContainer.Instance.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();

            IocContainer.Instance.AddAllAssemblies();

            services.AddSingleton<IDependencyResolver>(s => new
                FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<ISchema, TaskManagerSchema>();
            services.AddSingleton<TaskManagerQuery>();
            services.AddSingleton<TaskManagerMutation>();

            services.AddSingleton<BoardGraphType>();
            services.AddSingleton<BoardInputGraphType>();
            services.AddSingleton<ColumnGraphType>();
            services.AddSingleton<ColumnInputGraphType>();
            services.AddSingleton<TicketGraphType>();
            services.AddSingleton<TicketInputGraphType>();
            services.AddSingleton<TicketDetailsGraphType>();
            services.AddSingleton<CheckListGraphType>();
            services.AddSingleton<CheckListItemGraphType>();

            services.AddTransient<ITaskManagerDataProvider, TaskManagerDataProvider>();
            services.AddTransient<ITaskManagerDataMutator, TaskManagerDataMutator>();

            services.AddCors(o => o.AddPolicy("AllowOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            services.AddGraphQL();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AllowOrigins");

            //app.UseHttpsRedirection();
            //app.UseRouting();
            //app.UseAuthorization();

            //app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            app.UseGraphQL<ISchema>("/graphql");
            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });
        }
    }
}
