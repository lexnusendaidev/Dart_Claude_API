using DART_Claude.Business.Services;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Moq;

namespace DART_Claude.Tests.Business;

[TestClass]
public sealed class LookupServiceTests
{
    [TestMethod]
    public async Task GetLookupsAsync_ReturnsAllFiveListsMappedFromRepository()
    {
        Mock<IDartAppType> appType = new();
        appType.Setup(entity => entity.TypeId).Returns(1);
        appType.Setup(entity => entity.TypeName).Returns("Internal Application");

        Mock<IDartCriticality> criticality = new();
        criticality.Setup(entity => entity.CritId).Returns(1);
        criticality.Setup(entity => entity.CritName).Returns("Customer Facing");

        Mock<IDartSdlcPhase> sdlcPhase = new();
        sdlcPhase.Setup(entity => entity.SdlcId).Returns((short)7);
        sdlcPhase.Setup(entity => entity.SdlcName).Returns("Maintenance");

        Mock<IDartPathType> pathType = new();
        pathType.Setup(entity => entity.PathTypeId).Returns(1);
        pathType.Setup(entity => entity.PathTypeName).Returns("Development");

        Mock<IAppCurrentEmployee> employee = new();
        employee.Setup(entity => entity.EmpId).Returns((short)1);
        employee.Setup(entity => entity.EmpPreferredName).Returns("CM");

        Mock<ILookupRepository> repository = new();
        repository.Setup(repo => repo.GetAppTypesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartAppType> { appType.Object });
        repository.Setup(repo => repo.GetCriticalitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartCriticality> { criticality.Object });
        repository.Setup(repo => repo.GetSdlcPhasesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartSdlcPhase> { sdlcPhase.Object });
        repository.Setup(repo => repo.GetPathTypesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IDartPathType> { pathType.Object });
        repository.Setup(repo => repo.GetEmployeesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<IAppCurrentEmployee> { employee.Object });

        LookupService service = new(repository.Object);

        LookupsResponse response = await service.GetLookupsAsync(CancellationToken.None);

        Assert.HasCount(1, response.AppTypes);
        Assert.AreEqual("Internal Application", response.AppTypes[0].Name);
        Assert.HasCount(1, response.Criticalities);
        Assert.AreEqual("Customer Facing", response.Criticalities[0].Name);
        Assert.HasCount(1, response.SdlcPhases);
        Assert.AreEqual(7, response.SdlcPhases[0].Id);
        Assert.HasCount(1, response.PathTypes);
        Assert.HasCount(1, response.Employees);
        Assert.AreEqual("CM", response.Employees[0].Name);
    }
}
