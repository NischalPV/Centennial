﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Centennial.Api.Application.IntegrationEvents;
using Centennial.Api.Data;
using Centennial.Api.Infrastructure.AutofacModules;
using Centennial.Api.Infrastructure.Filters;
using Centennial.Api.Infrastructure.Services;
using Centennial.BuildingBlocks.EventBus;
using Centennial.BuildingBlocks.EventBus.Abstractions;
using Centennial.BuildingBlocks.EventBusRabbitMQ;
using Centennial.BuildingBlocks.IntegrationEventLogEF.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Centennial.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllersWithViews();
            services
                .AddCustomMvc()
                .AddHealthChecks(Configuration)
                .AddCustomDbContext(Configuration)
                .AddCustomSwagger(Configuration)
                .AddCustomIntegrations(Configuration)
                .AddCustomConfiguration(Configuration)
                .AddEventBus(Configuration)
                .AddCustomAuthentication(Configuration);

            services.AddScopedServices();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ApplicationModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            // this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            var pathBase = Configuration["PATH_BASE"];
            var oAuthClientId = Configuration["OAuthClientId"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Centennial.API V1");
                   c.OAuthClientId($"{oAuthClientId}");
                   c.OAuthAppName("Centennial API Swagger UI");
               });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            ConfigureAuth(app);

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                //endpoints.MapRazorPages();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });

            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<BuildingBlocks.EventBus.Abstractions.IEventBus>();
        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            //if (Configuration.GetValue<bool>("UseLoadTest"))
            //{
            //    app.UseMiddleware<ByPassAuthMiddleware>();
            //}

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                //.AddApplicationPart(typeof(MasterdataController).Assembly)
                .AddNewtonsoftJson();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder
                .AddSqlServer(
                    configuration["DefaultConnection"],
                    name: "DevicesDb-check",
                    tags: new string[] { "devicesdb" });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CentennialDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            }, ServiceLifetime.Scoped);

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Centannial - HTTP API",
                    Version = "v1",
                    Description = "The Centennial Service HTTP API"
                });

                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
                    Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows()
                    {
                        Implicit = new Microsoft.OpenApi.Models.OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("ExternalIdentityUrl")}/connect/authorize"),
                            TokenUrl = new Uri($"{configuration.GetValue<string>("ExternalIdentityUrl")}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                {"API", "API Scope" }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IApiIntegrationEventService, ApiIntegrationEventService>();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<AppSettings>(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            //var identityUrl = "https://uecpl-identity";
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            //Console.WriteLine($"IdentityUrl : {identityUrl}");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "centennial-api";
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                //options.Audience = "devices";
            });

            return services;
        }
    }

}
