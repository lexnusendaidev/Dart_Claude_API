using DART_Claude.Business.Services;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class FeedbackServiceTests
{
    [TestMethod]
    public async Task GetFeedbackForApplicationAsync_ReturnsResponsesMappedFromRepository()
    {
        DateTime createDate = new(2021, 6, 14);

        Mock<IDartFeedback> feedback = new();
        feedback.Setup(entity => entity.FbId).Returns(1);
        feedback.Setup(entity => entity.FbDescription).Returns("Test Feedback");
        feedback.Setup(entity => entity.FbIsActive).Returns(false);
        feedback.Setup(entity => entity.FbFollowupComplete).Returns(true);
        feedback.Setup(entity => entity.FbWasImplemented).Returns(false);
        feedback.Setup(entity => entity.FbImplementedInVersion).Returns((string?)null);
        feedback.Setup(entity => entity.FbImplementedComments).Returns((string?)null);
        feedback.Setup(entity => entity.CreateDate).Returns(createDate);

        Mock<IFeedbackRepository> repository = new();
        repository
            .Setup(repo => repo.GetByApplicationIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartFeedback> { feedback.Object });

        FeedbackService service = new(repository.Object);

        List<FeedbackResponse> responses = await service.GetFeedbackForApplicationAsync(1, CancellationToken.None);

        Assert.HasCount(1, responses);
        Assert.AreEqual(1, responses[0].Id);
        Assert.AreEqual("Test Feedback", responses[0].Description);
        Assert.IsFalse(responses[0].IsActive);
        Assert.IsTrue(responses[0].FollowupComplete.GetValueOrDefault());
        Assert.AreEqual(createDate, responses[0].CreateDate);
    }
}
