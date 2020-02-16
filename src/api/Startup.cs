using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using HotChocolate;
using api.Graph.Queries;
using api.Data.Repositories;
using api.Data.DbContexts;
using HotChocolate.AspNetCore;

namespace api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // Registering services within the Dependency Injection (DI) container allows you to use those services in the request pipline.
        // It also allows other services in the DI container to use those services.
        // Services in this case are often simply objects that have been instantiated whose members you can access within the request pipleine.
        public void ConfigureServices(IServiceCollection services)
        {
            // Connect to the InMemory database and make it available to other services
            services.AddDbContext<ToDoDbContext>();

            // sets-up authentication and authorization for requests
            // change configuration options in appsettings.json
            // http://docs.identityserver.io/en/latest/topics/apis.html
            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = this._config.GetSection("Authentication:Authority").ToString(); // Determines the authorization server used.
                    options.Audience = this._config.GetSection("Authentication:Audience").ToString(); // Determines the audience that this API requires.
                    options.RequireHttpsMetadata = false;
                });

            // register respositories to be used by Query
            // for an explanation of AddTransient vs AddScoped vs AddSigleton, refer to this:
            // https://stackoverflow.com/a/38139500/2344773
            services.AddTransient<IToDoRepository, ToDoRepository>();

            // define the GraphQL schema
            services.AddGraphQL(SchemaBuilder.New() // This instantiates a new schema.
                .AddAuthorizeDirectiveType() // This addes authorization directives to the new schema.
                .ModifyOptions(o => o.RemoveUnreachableTypes = true) // I believe this attempts to traverse our schema and exclude types that it cannot reach from the final schema.
                .AddQueryType(d => d.Name("Query")) // This adds a "bodyless" type of Query which we later extend with the [ExtendObjectType(Name = "Query")] attribute in queries.
                .AddType<ToDoQuery>() // This adds the X type to our schema and contains an exmaple of the [ExtendObjectType(Name = "Query")] attribute.
                .Create()); // This creates the schema with the configured options.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Here is an excellent video about the request pipeline and middleware: 
        // https://youtu.be/HCxAERjO4C4
        //
        // Other recommended reading about the request pipeline:
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
        // https://thomaslevesque.com/2018/03/27/understanding-the-asp-net-core-middleware-pipeline/
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // IWebHostEnvironment contains information about the environment the application is running in.
            // env.IsDevelopment() returns true if the ASPNETCORE_ENVIRONMENT environment variable is set to Development
            // e.g. when you're running the app locally in kestrel
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1
            //
            // Here are some resources for learning more about Kestrel if you're interested in that:
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?tabs=aspnetcore2x&view=aspnetcore-3.1
            // https://stackoverflow.com/a/46878663/2344773
            if (env.IsDevelopment())
            {
                // 
                app.UseDeveloperExceptionPage();
            }

            // UseRouting must be called before UseAuthentication or UseAuthorization
            // so that route information is available for authentication and authorization decisions
            // https://stackoverflow.com/questions/58475596/why-does-useauthentication-have-to-be-placed-after-userouting-and-not-before/60051604#60051604
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGraphQL();

            if (env.IsDevelopment())
            {
                // This allows use the GraphQL playground which is accessible at /playground by default
                app.UsePlayground();
            }
        }
    }
}
