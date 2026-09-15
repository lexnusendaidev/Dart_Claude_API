using DART_Claude.Business.Services;
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
}
