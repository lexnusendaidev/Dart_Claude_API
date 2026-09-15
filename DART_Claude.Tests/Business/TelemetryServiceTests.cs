using DART_Claude.Business.Services;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class TelemetryServiceTests
{
    [TestMethod]
    public async Task GetTelemetryForApplicationAsync_ReturnsResponsesMappedFromRepository()
    {
        DateTime createDate = new(2026, 1, 1);

        Mock<IDartTelemetry> telemetry = new();
        telemetry.Setup(entity => entity.TeleId).Returns(1);
        telemetry.Setup(entity => entity.TeleMethodName).Returns("GetApplications");
        telemetry.Setup(entity => entity.TeleCreateDate).Returns(createDate);

        Mock<ITelemetryRepository> repository = new();
        repository
            .Setup(repo => repo.GetByApplicationIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartTelemetry> { telemetry.Object });

        TelemetryService service = new(repository.Object);

        List<TelemetryResponse> responses = await service.GetTelemetryForApplicationAsync(1, CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(1, responses[0].Id);
        Assert.AreEqual("GetApplications", responses[0].MethodName);
        Assert.AreEqual(createDate, responses[0].CreateDate);
    }
}
