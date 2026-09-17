using DART_Claude.Business.Services;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class PathServiceTests
{
    [TestMethod]
    public async Task GetPathsForApplicationAsync_ReturnsResponsesMappedFromRepository()
    {
        Mock<IPathListItem> path = new();
        path.Setup(entity => entity.PathId).Returns(1);
        path.Setup(entity => entity.PathTypeName).Returns("Development");
        path.Setup(entity => entity.PathLocation).Returns("SomeUrl");

        Mock<IPathRepository> repository = new();
        repository
            .Setup(repo => repo.GetByApplicationIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IPathListItem> { path.Object });

        Mock<IApplicationRepository> applicationRepository = new();

        PathService service = new(repository.Object, applicationRepository.Object);

        List<PathResponse> responses = await service.GetPathsForApplicationAsync(1, CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(1, responses[0].PathId);
        Assert.AreEqual("Development", responses[0].PathTypeName);
        Assert.AreEqual("SomeUrl", responses[0].PathLocation);
    }
}
