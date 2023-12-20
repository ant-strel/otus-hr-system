using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;

using FluentValidation;

using HR_Portal_API.Models.Request;
using HR_Portal_API.Validations;

using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.CustomRepositories;

using Services.Abstractions;
using Services.Impl;

namespace HR_Portal_API;

public static class Configuration
{
    public static IServiceCollection ConfigServices(WebApplicationBuilder builder) 
    { 
        builder ??= WebApplication.CreateBuilder(); // создаём при использовании в тестах

        builder.Services.AddAutoMapper(typeof(Program));    // подключаем AutoMapper для упрощения работы с сущностями "Entities <-> DTO"

        #region подключаем работу с Сущностями в зависимости
        builder.Services.AddScoped<ICandidateService, CandidateService>();
        builder.Services.AddScoped<IJobReplyService, JobReplyService>();
        builder.Services.AddScoped<IJobService, JobService>();
        builder.Services.AddScoped<IStatusService, StatusService>();
        builder.Services.AddScoped<IRepository<JobReply>, JobReplyRepository>();
        builder.Services.AddScoped<IRepository<Candidate>, CandidateRepository>();
        builder.Services.AddScoped<IRepository<Job>, JobRepository>();
        builder.Services.AddScoped<IRepository<Status>, StatusRepository>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        #endregion

        #region подключаем Валидаторы
        builder.Services.AddScoped<IValidator<CandidateRequest>, CandidateRequestValidator>();
        builder.Services.AddScoped<IValidator<CandidateFullRequest>, CandidateFullRequestValidator>();
        builder.Services.AddScoped<IValidator<JobRequest>, JobRequestValidator>();
        builder.Services.AddScoped<IValidator<JobFullRequest>, JobFullRequestValidator>();
        #endregion

        return builder.Services;
    }
}
