using Domain.Entities.Abstractions.Repositories;

using HR_WorkFlow_API.Bus;

using IdentityModel;

using Infrastructure.EntityFramework;

using MassTransit;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MongoDB.Driver;

using Services.Abstractions;
using Services.Impl;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services
        .AddSingleton<IMongoClient>(_ => new MongoClient(builder.Configuration["MongoDb:ConnectionString"]))
        .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IMongoClient>().GetDatabase(builder.Configuration["MongoDb:Database"]))
        .AddScoped(serviceProvider => serviceProvider.GetRequiredService<IMongoClient>().StartSession());


builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));
builder.Services.AddScoped<IDbInitializer, MongoDbInitializer>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
//builder.Services.AddScoped<IDbInitializer, EfDbInitializer>();
//builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(builder.Configuration.Get<ApplicationSettings>().ConnectionString).UseLazyLoadingProxies());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["Jwt:Issuer"];
            options.MetadataAddress = "http://hr_identity/.well-known/openid-configuration";
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                NameClaimType = JwtClaimTypes.Name,
                RoleClaimType = JwtClaimTypes.Role,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
            options.MapInboundClaims = false;
        });


builder.Services.AddAuthorization(optionns =>
{
    optionns.AddPolicy("Administrator", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Administrator");
    });
    optionns.AddPolicy("User", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("User");
    });
    optionns.AddPolicy("All", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Demo Swagger API v1",
        Title = "Swagger Workflow",
        Version = "1.0.0"
    });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("http://localhost:86/connect/token"),
                Scopes = new Dictionary<string, string>
                        {
                            {"SwaggerAPI_WF", "Swagger API DEMO WF" }
                        }
            }
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
});

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<IJobPostingService, JobPostingService>();
builder.Services.AddScoped<IJobPostingStatusService, JobPostingStatusService>();
builder.Services.AddScoped<IResolutionService, ResolutionService>();
builder.Services.AddScoped<IStatusService, StatusService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "react",
        builder =>
        {
            builder.WithOrigins("*").
            WithHeaders("*").
            WithMethods("*");
        });
});

builder.Services.AddMassTransit(options => {

    options.AddConsumer<JobPostingConsumer>();

    options.UsingRabbitMq((context, cfg) => {
        cfg.Host(builder.Configuration["RMQSettings:Host"],
            h =>
            {
                h.Username(builder.Configuration["RMQSettings:Login"]);
                h.Password(builder.Configuration["RMQSettings:Password"]);
            });
        cfg.ReceiveEndpoint("Bus:CreatingJobPostingDto", e =>
        {
            e.ConfigureConsumer<JobPostingConsumer>(context);
            e.UseMessageRetry(r =>
            {
                r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            });

        });

    });
});


var dbInitializer = builder.Services.BuildServiceProvider().GetService<IDbInitializer>();

if (dbInitializer != null)
{
    dbInitializer.InitializeDb();
}

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            c.OAuthClientId("client_id_swagger_wf");
            c.OAuthClientSecret("client_secret_swagger_wf");
        });
    app.UseDeveloperExceptionPage();
}
app.UseCors("react");
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "Workflow Online!");

app.Run();

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        CreateHostBuilder(args).Build().Run();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args)
//    {
//        return Host.CreateDefaultBuilder(args)
//            .ConfigureWebHostDefaults(webBuilder =>
//            {
//                webBuilder.UseStartup<Startup>();
//                webBuilder.ConfigureAppConfiguration((hostingContext, config) => { });
//            });
//    }
//}