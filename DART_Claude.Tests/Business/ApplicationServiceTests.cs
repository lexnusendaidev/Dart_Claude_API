using DART_Claude.Business.Services;
using DART_Claude.Common.Constants;
using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class ApplicationServiceTests
{
    [TestMethod]
    public async Task GetApplicationsAsync_ReturnsResponsesMappedFromRepository()
    {
        DateTime sdlcCheckDate = new(2023, 12, 8);

        Mock<IApplicationListItem> application = new();
        application.Setup(entity => entity.AppId).Returns(1);
        application.Setup(entity => entity.ApplicationName).Returns("Santa Tracker");
        application.Setup(entity => entity.Criticality).Returns("Customer Facing");
        application.Setup(entity => entity.AppType).Returns("Internal Application");
        application.Setup(entity => entity.PrimaryDeveloper).Returns("CM");
        application.Setup(entity => entity.SecondaryDeveloper).Returns((string?)null);
        application.Setup(entity => entity.Analyst).Returns((string?)null);
        application.Setup(entity => entity.SdlcPhase).Returns("Maintenance");
        application.Setup(entity => entity.SdlcCheckDate).Returns(sdlcCheckDate);

        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IApplicationListItem> { application.Object });

        ApplicationService service = new(repository.Object);

        List<ApplicationResponse> responses = await service.GetApplicationsAsync(CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(1, responses[0].Id);
        Assert.AreEqual("Santa Tracker", responses[0].Name);
        Assert.AreEqual("Customer Facing", responses[0].Criticality);
        Assert.AreEqual("Internal Application", responses[0].AppType);
        Assert.AreEqual("CM", responses[0].PrimaryDeveloper);
        Assert.IsNull(responses[0].SecondaryDeveloper);
        Assert.AreEqual("Maintenance", responses[0].SdlcPhase);
        Assert.AreEqual(sdlcCheckDate, responses[0].SdlcCheckDate);
    }

    [TestMethod]
    public async Task CreateApplicationAsync_MapsRequestIntoNewApplicationAndWrapsReturnedId()
    {
        DateTime sdlcCheckDate = new(2024, 3, 15);
        CreateApplicationRequest request = new()
        {
            Name = "Santa Tracker",
            CurrentVersion = 1.2m,
            Description = "Tracks Santa's route",
            AppTypeId = 2,
            CriticalityId = 3,
            PrimaryDeveloperEmpId = 10,
            SecondaryDeveloperEmpId = 11,
            AnalystEmpId = 12,
            SdlcPhaseId = 4,
            SdlcCheckDate = sdlcCheckDate,
            FriendlyName = "Santa",
            AllowFeedback = true,
        };

        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.CreateAsync(It.IsAny<NewApplication>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(42);

        ApplicationService service = new(repository.Object);

        CreateApplicationResponse response = await service.CreateApplicationAsync(request, CancellationToken.None);

        Assert.AreEqual(42, response.Id);
        repository.Verify(
            repo => repo.CreateAsync(
                It.Is<NewApplication>(application =>
                    application.Name == request.Name
                    && application.CurrentVersion == request.CurrentVersion
                    && application.Description == request.Description
                    && application.AppTypeId == request.AppTypeId
                    && application.CriticalityId == request.CriticalityId
                    && application.PrimaryDeveloperEmpId == request.PrimaryDeveloperEmpId
                    && application.SecondaryDeveloperEmpId == request.SecondaryDeveloperEmpId
                    && application.AnalystEmpId == request.AnalystEmpId
                    && application.SdlcPhaseId == request.SdlcPhaseId
                    && application.SdlcCheckDate == request.SdlcCheckDate
                    && application.FriendlyName == request.FriendlyName
                    && application.AllowFeedback == request.AllowFeedback
                    && application.CreatedByEmpId == PlaceholderIdentity.EmployeeId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
