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
        Mock<IDartApplication> application = new();
        application.Setup(entity => entity.AppId).Returns(1);
        application.Setup(entity => entity.AppName).Returns("Santa Tracker");

        Mock<IApplicationRepository> repository = new();
        repository
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartApplication> { application.Object });

        ApplicationService service = new(repository.Object);

        List<ApplicationResponse> responses = await service.GetApplicationsAsync(CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(1, responses[0].Id);
        Assert.AreEqual("Santa Tracker", responses[0].Name);
    }
}
