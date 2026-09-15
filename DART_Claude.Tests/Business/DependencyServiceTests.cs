using DART_Claude.Business.Services;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class DependencyServiceTests
{
    [TestMethod]
    public async Task GetDependenciesForApplicationAsync_ReturnsResponsesMappedFromRepository()
    {
        Mock<IDependencyListItem> dependency = new();
        dependency.Setup(entity => entity.DependOnAppId).Returns(2);
        dependency.Setup(entity => entity.DependOnAppName).Returns("Elf Scheduler");

        Mock<IDependencyRepository> repository = new();
        repository
            .Setup(repo => repo.GetByApplicationIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDependencyListItem> { dependency.Object });

        DependencyService service = new(repository.Object);

        List<DependencyResponse> responses = await service.GetDependenciesForApplicationAsync(1, CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(2, responses[0].DependOnAppId);
        Assert.AreEqual("Elf Scheduler", responses[0].DependOnAppName);
    }
}
