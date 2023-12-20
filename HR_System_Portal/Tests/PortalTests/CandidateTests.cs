using AutoMapper;

using FluentValidation;

using HR_Portal_API.Models.Request;

using Microsoft.Extensions.DependencyInjection;

using Services.Contracts.DTO;

using Xunit;

namespace PortalTests;

public class CandidateTests : IClassFixture<TestFixture>
{
    private readonly CandidateFullDto baseCandidateDTO;
    private readonly IServiceProvider serviceProvider;
    private readonly IMapper? mapper;
    private readonly IValidator<CandidateRequest>? validatorCR;
    private readonly IValidator<CandidateFullRequest>? validatorCFR;

    public CandidateTests(TestFixture testFixture)
    {
        baseCandidateDTO =
                   new CandidateFullDto()
                   {
                       Id = Guid.Parse("8603f7ea-e63f-426b-8733-41debed0c50c"),
                       FirstName = "SomeFirstName",
                       LastName = "SomeLastName",
                       Surname = "SomeSurname",
                       Address = "SomeAddress",
                       Age = 25
                   };

        serviceProvider = testFixture.ServiceProvider;
        mapper          = serviceProvider.GetService<IMapper>();
        validatorCR     = serviceProvider.GetService<IValidator<CandidateRequest>>();
        validatorCFR    = serviceProvider.GetService<IValidator<CandidateFullRequest>>();
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_FirstName()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.FirstName = "";    // поле 'FirstName' должно быть заполнено иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.Equal("Незаполненное поле!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Незаполненное поле!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_LastName()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.LastName = "";    // поле 'LastName' должно быть заполнено иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.Equal("Незаполненное поле!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Незаполненное поле!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_Surname()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.Surname = "";    // поле 'Surname' должно быть заполнено иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.Equal("Незаполненное поле!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Незаполненное поле!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Incorrect_Age_Greater_Than_100()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.Age = 100;    // возраст должен быть МЕНЬШЕ 100 иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.Equal("Некорректный возраст!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Некорректный возраст!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Incorrect_Age_Less_Than_17()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.Age = 16;    // возраст должен быть БОЛЬШЕ 16 иначе вернёт ошибку (это и проверяем)

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.Equal("Некорректный возраст!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("Некорректный возраст!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_Warning_For_Empty_GUID()
    {
        //Arrange
        var candidate =
            new CandidateFullDto()
            {
                FirstName = "SomeFirstName",
                LastName = "SomeLastName",
                Surname = "SomeSurname",
                Address = "SomeAddress",
                Age = 25
            };

        //Act
        var validationResult = validatorCFR!.Validate(mapper!.Map<CandidateFullRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.Equal("GUID обязателен!", validationResult.Errors.FirstOrDefault(ex =>
                    ex.ErrorMessage.Contains("GUID обязателен!"))?.ToString());
    }

    [Fact]
    public void Validate_Should_Return_True_For_Request_Valid_Data()
    {
        //Arrange
        var candidate =
            new CandidateFullDto()
            {
                FirstName = "SomeFirstName",
                LastName = "SomeLastName",
                Surname = "SomeSurname",
                Address = "SomeAddress",
                Age = 25
            };

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void Validate_Should_Return_True_For_FullRequest_Valid_Data()
    {
        //Arrange
        var candidate = baseCandidateDTO;

        //Act
        var validationResult = validatorCFR!.Validate(mapper!.Map<CandidateFullRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void Validate_Should_Return_False_For_Request_Invalid_Data()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.FirstName = "";

        //Act
        var validationResult = validatorCR!.Validate(mapper!.Map<CandidateRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public void Validate_Should_Return_False_For_FullRequest_Invalid_Data()
    {
        //Arrange
        var candidate = baseCandidateDTO;
        candidate.FirstName = "";

        //Act
        var validationResult = validatorCFR!.Validate(mapper!.Map<CandidateFullRequest>(candidate));

        //Assert
        Assert.NotNull(candidate);
        Assert.False(validationResult.IsValid);
    }
}
