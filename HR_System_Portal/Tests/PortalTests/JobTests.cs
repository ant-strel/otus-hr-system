using AutoMapper;
using Domain.Entities.Entities;
using FluentValidation;
using HR_Portal_API.Models.Request;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PortalTests;

public class JobTests : IClassFixture<TestFixture>
{
    private readonly JobDto baseJobDTO;
    private readonly IServiceProvider serviceProvider;
    private readonly IMapper? mapper;
    private readonly IValidator<JobRequest>? validatorJR;
    private readonly IValidator<JobFullRequest>? validatorJFR;

    public JobTests(TestFixture testFixture)
    {
        baseJobDTO =
            new JobDto()
            {
                Id = Guid.Parse("e1bc0ac8-5867-42ef-b279-6f2dcac3e39b"),
                Name = "SomeName",
                Description = "SomeDescription"
            };

        serviceProvider = testFixture.ServiceProvider;
        mapper = serviceProvider.GetService<IMapper>();
        validatorJR = serviceProvider.GetService<IValidator<JobRequest>>();
        validatorJFR = serviceProvider.GetService<IValidator<JobFullRequest>>();
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Short_Name()
    {
        //Arrange
        var job = baseJobDTO;
        job.Name = "123";    // поле 'Name' должно быть 4-100 символов иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorJR!.Validate(mapper!.Map<JobRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.Equal("Текст должен быть от 4 до 100 символов!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Текст должен быть от 4 до 100 символов!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Short_Description()
    {
        //Arrange
        var job = baseJobDTO;
        job.Description = "123";    // поле 'Description' должно быть 4-100 символов иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorJR!.Validate(mapper!.Map<JobRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.Equal("Текст должен быть от 4 до 100 символов!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Текст должен быть от 4 до 100 символов!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_Name()
    {
        //Arrange
        var job = baseJobDTO;
        job.Name = "";    // поле 'Name' должно быть заполнено иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorJR!.Validate(mapper!.Map<JobRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.Equal("Незаполненное поле!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Незаполненное поле!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_Description()
    {
        //Arrange
        var job = baseJobDTO;
        job.Description = "";    // поле 'Description' должно быть заполнено иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorJR!.Validate(mapper!.Map<JobRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.Equal("Незаполненное поле!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Незаполненное поле!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_GUID()
    {
        //Arrange
        var job =
            new JobDto()
            {
                Name = "SomeManager",
                Description = "SomeDescription"
            };

        //Act
        var validationResult = validatorJFR!.Validate(mapper!.Map<JobFullRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.Equal("GUID обязателен!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("GUID обязателен!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_False_For_Request_Invalid_Data()
    {
        //Arrange
        var job = baseJobDTO;
        job.Name = "";

        //Act
        var validationResult = validatorJR!.Validate(mapper!.Map<JobRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public void Validate_Should_Return_False_For_FullRequest_Invalid_Data()
    {
        //Arrange
        var job = baseJobDTO;
        job.Name = "";

        //Act
        var validationResult = validatorJFR!.Validate(mapper!.Map<JobFullRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public void Validate_Should_Return_True_For_Request_Valid_Data()
    {
        //Arrange
        var job =
            new JobDto()
            {
                Name = "SomeName",
                Description = "SomeDescription"
            };

        //Act
        var validationResult = validatorJR!.Validate(mapper!.Map<JobRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void Validate_Should_Return_True_For_FullRequest_Valid_Data()
    {
        //Arrange
        var job = baseJobDTO;

        //Act
        var validationResult = validatorJFR!.Validate(mapper!.Map<JobFullRequest>(job));

        //Assert
        Assert.NotNull(job);
        Assert.True(validationResult.IsValid);
    }

}
