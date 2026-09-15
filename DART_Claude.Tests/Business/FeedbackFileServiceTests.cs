using DART_Claude.Business.Services;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class FeedbackFileServiceTests
{
    [TestMethod]
    public async Task GetFilesForFeedbackAsync_ReturnsResponsesMappedFromRepository()
    {
        Mock<IDartFeedbackFile> file = new();
        file.Setup(entity => entity.FfId).Returns(1L);
        file.Setup(entity => entity.FfFilePathway).Returns("SomeFileLocationName");

        Mock<IFeedbackFileRepository> repository = new();
        repository
            .Setup(repo => repo.GetByFeedbackIdAsync(6, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartFeedbackFile> { file.Object });

        FeedbackFileService service = new(repository.Object);

        List<FeedbackFileResponse> responses = await service.GetFilesForFeedbackAsync(6, CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(1L, responses[0].Id);
        Assert.AreEqual("SomeFileLocationName", responses[0].FilePathway);
    }
}
