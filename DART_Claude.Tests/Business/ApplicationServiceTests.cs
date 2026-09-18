using DART_Claude.Business.Services;
using DART_Claude.Common.Constants;
using DART_Claude.Common.Exceptions;
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

    [TestMethod]
    public async Task GetApplicationDetailAsync_ReturnsResponseMappedFromRepository()
    {
        DateTime sdlcCheckDate = new(2024, 3, 15);

        Mock<IApplicationDetail> application = new();
        application.Setup(entity => entity.Id).Returns(1);
        application.Setup(entity => entity.Name).Returns("Santa Tracker");
        application.Setup(entity => entity.CurrentVersion).Returns(1.2m);
        application.Setup(entity => entity.Description).Returns("Tracks Santa's route");
        application.Setup(entity => entity.AppTypeId).Returns(2);
        application.Setup(entity => entity.CriticalityId).Returns(3);
        application.Setup(entity => entity.PrimaryDeveloperEmpId).Returns((short)10);
        application.Setup(entity => entity.SecondaryDeveloperEmpId).Returns((short?)11);
        application.Setup(entity => entity.AnalystEmpId).Returns((short?)12);
        application.Setup(entity => entity.SdlcPhaseId).Returns((short)4);
        application.Setup(entity => entity.SdlcCheckDate).Returns(sdlcCheckDate);
        application.Setup(entity => entity.FriendlyName).Returns("Santa");
        application.Setup(entity => entity.AllowFeedback).Returns(true);

        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.GetDetailByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(application.Object);

        ApplicationService service = new(repository.Object);

        ApplicationDetailResponse response = await service.GetApplicationDetailAsync(1, CancellationToken.None);

        Assert.AreEqual(1, response.Id);
        Assert.AreEqual("Santa Tracker", response.Name);
        Assert.AreEqual(1.2m, response.CurrentVersion);
        Assert.AreEqual("Tracks Santa's route", response.Description);
        Assert.AreEqual(2, response.AppTypeId);
        Assert.AreEqual(3, response.CriticalityId);
        Assert.AreEqual((short)10, response.PrimaryDeveloperEmpId);
        Assert.AreEqual((short)11, response.SecondaryDeveloperEmpId);
        Assert.AreEqual((short)12, response.AnalystEmpId);
        Assert.AreEqual((short)4, response.SdlcPhaseId);
        Assert.AreEqual(sdlcCheckDate, response.SdlcCheckDate);
        Assert.AreEqual("Santa", response.FriendlyName);
        Assert.IsTrue(response.AllowFeedback);
    }

    [TestMethod]
    public async Task GetApplicationDetailAsync_ThrowsWhenApplicationNotFound()
    {
        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.GetDetailByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IApplicationDetail?)null);

        ApplicationService service = new(repository.Object);

        await Assert.ThrowsExactlyAsync<EntityNotFoundException>(
            () => service.GetApplicationDetailAsync(999, CancellationToken.None));
    }

    [TestMethod]
    public async Task UpdateApplicationAsync_MapsRequestIntoUpdatedApplicationAndReturnsTrue()
    {
        DateTime sdlcCheckDate = new(2024, 3, 15);
        UpdateApplicationRequest request = new()
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

        Mock<IApplicationDetail> existingApplication = new();
        existingApplication.Setup(entity => entity.Id).Returns(1);

        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.GetDetailByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingApplication.Object);

        ApplicationService service = new(repository.Object);

        bool result = await service.UpdateApplicationAsync(1, request, CancellationToken.None);

        Assert.IsTrue(result);
        repository.Verify(
            repo => repo.UpdateAsync(
                It.Is<UpdatedApplication>(application =>
                    application.Id == 1
                    && application.Name == request.Name
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
                    && application.UpdatedByEmpId == PlaceholderIdentity.EmployeeId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [TestMethod]
    public async Task UpdateApplicationAsync_ThrowsWhenApplicationNotFound()
    {
        UpdateApplicationRequest request = new()
        {
            Name = "Santa Tracker",
            FriendlyName = "Santa",
        };

        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.GetDetailByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IApplicationDetail?)null);

        ApplicationService service = new(repository.Object);

        await Assert.ThrowsExactlyAsync<EntityNotFoundException>(
            () => service.UpdateApplicationAsync(999, request, CancellationToken.None));

        repository.Verify(
            repo => repo.UpdateAsync(It.IsAny<UpdatedApplication>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
