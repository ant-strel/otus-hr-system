using HR_Portal_API;
using HR_Portal_API.Bus;
using HR_Portal_API.Middlewares;

using IdentityModel;

using Infrastructure.EntityFramework;

using MassTransit;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DatabaseContext>();   // инициализация и настройка внутри контекста
builder.Services.AddResponseCaching();


#region Authentication and Authorization
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
#endregion

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Demo Swagger API v1",
        Title = "Swagger Portal",
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
                            {"SwaggerAPI_Portal", "Swagger API DEMO Portal" }
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

});      // подключаем Swagger c авторизацией, запуск: https://localhost/swagger/index.html

// сервисы одновременно необходимые и для IoC
// и для тестирования вынесены в отдельный метод
Configuration.ConfigServices(builder);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "react",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000").
                    WithHeaders("*").
                    WithMethods("*")
                    .AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        });
});

builder.Services.AddMassTransit(options => {

    options.AddConsumer<StatusChangedConsumer>();

    options.UsingRabbitMq((context, cfg) => {
        cfg.Host(builder.Configuration["RMQSettings:Host"],
            h =>
            {
                h.Username(builder.Configuration["RMQSettings:Login"]);
                h.Password(builder.Configuration["RMQSettings:Password"]);
            });
        cfg.ReceiveEndpoint("Bus:JobPostingStatusChangedDto", e =>
        {
            e.ConfigureConsumer<StatusChangedConsumer>(context);
            e.UseMessageRetry(r =>
            {
                r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            });

        });

    });
});

builder.Services.AddScoped<IDbInitializer, EfDbInitializer>();

var dbInitializer = builder.Services.BuildServiceProvider().GetService<IDbInitializer>();

if (dbInitializer != null)
{
    dbInitializer.InitializeDb();
}
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
#if DEBUG
    var startupPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);
    var logFilePath = Path.Combine(startupPath!, "logs", ".log");

    loggerConfiguration.WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day);
#else
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
#endif
});
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            c.OAuthClientId("client_id_swagger_portal");
            c.OAuthClientSecret("client_secret_swagger_portal");
        });
    app.UseDeveloperExceptionPage();
}

app.UseCors("react");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomMiddlewares();
app.MapControllers();
app.UseResponseCaching();

app.MapHub<SrHub>("/srbus");

app.Run();
