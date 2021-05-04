using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.Models;
using DFC.App.Lmi.Delta.Report.Services;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.Lmi.Delta.Report.UnitTests.ServicesTests
{
    [Trait("Category", "Delta Report service Unit Tests")]
    public class DeltaReportServiceTests
    {
        private readonly ILogger<DeltaReportService> fakeLogger = A.Fake<ILogger<DeltaReportService>>();
        private readonly IDeltaReportApiConnector fakeDeltaReportApiConnector = A.Fake<IDeltaReportApiConnector>();

        [Fact]
        public async Task DeltaReportServiceGetSummaryReturnsSuccess()
        {
            // Arrange
            const int resultsCount = 2;
            var expectedResults = A.CollectionOfFake<DeltaReportSummaryItemModel>(resultsCount);
            var dummyCachedDeltaReportModel = A.Dummy<CachedDeltaReportModel>();
            var service = new DeltaReportService(fakeLogger, fakeDeltaReportApiConnector, dummyCachedDeltaReportModel);

            A.CallTo(() => fakeDeltaReportApiConnector.GetSummaryAsync()).Returns(expectedResults);

            // Act
            var results = await service.GetSummaryAsync().ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportApiConnector.GetSummaryAsync()).MustHaveHappenedOnceExactly();

            A.Equals(expectedResults, results);
        }

        [Fact]
        public async Task DeltaReportServiceGetDetailReturnsSuccess()
        {
            // Arrange
            var expectedResult = A.Dummy<DeltaReportModel>();
            var dummyCachedDeltaReportModel = A.Dummy<CachedDeltaReportModel>();
            var service = new DeltaReportService(fakeLogger, fakeDeltaReportApiConnector, dummyCachedDeltaReportModel);

            A.CallTo(() => fakeDeltaReportApiConnector.GetDetailAsync(A<Guid>.Ignored)).Returns(expectedResult);

            // Act
            var result = await service.GetDetailAsync(Guid.NewGuid()).ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportApiConnector.GetDetailAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();

            A.Equals(expectedResult, result);
        }

        [Fact]
        public async Task DeltaReportServiceGetDetailFromCacheReturnsSuccess()
        {
            // Arrange
            var cachedDeltaReportModel = new CachedDeltaReportModel { DeltaReportModel = new DeltaReportModel { Id = Guid.NewGuid() } };
            var service = new DeltaReportService(fakeLogger, fakeDeltaReportApiConnector, cachedDeltaReportModel);

            // Act
            var result = await service.GetDetailAsync(cachedDeltaReportModel.DeltaReportModel.Id.Value).ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportApiConnector.GetDetailAsync(A<Guid>.Ignored)).MustNotHaveHappened();

            A.Equals(cachedDeltaReportModel.DeltaReportModel, result);
        }
    }
}