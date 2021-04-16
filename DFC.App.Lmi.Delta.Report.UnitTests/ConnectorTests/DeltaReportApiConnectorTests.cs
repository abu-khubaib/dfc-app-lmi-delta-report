using DFC.App.Lmi.Delta.Report.Connectors;
using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.Models;
using DFC.App.Lmi.Delta.Report.Models.ClientOptions;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.Lmi.Delta.Report.UnitTests.ConnectorTests
{
    [Trait("Category", "Delta Report API data connector Unit Tests")]
    public class DeltaReportApiConnectorTests
    {
        private readonly ILogger<DeltaReportApiConnector> fakeLogger = A.Fake<ILogger<DeltaReportApiConnector>>();
        private readonly HttpClient httpClient = new HttpClient();
        private readonly IApiDataConnector fakeApiDataConnector = A.Fake<IApiDataConnector>();
        private readonly DeltaReportApiConnector deltaReportApiConnector;
        private readonly DeltaReportApiClientOptions deltaReportClientOptions = new DeltaReportApiClientOptions
        {
            BaseAddress = new Uri("https://somewhere.com", UriKind.Absolute),
        };

        public DeltaReportApiConnectorTests()
        {
            deltaReportApiConnector = new DeltaReportApiConnector(fakeLogger, httpClient, fakeApiDataConnector, deltaReportClientOptions);
        }

        [Fact]
        public async Task DeltaReportApiConnectorTestsGetSummaryReturnsSuccess()
        {
            // arrange
            var expectedResults = new List<DeltaReportSummaryItemModel>
            {
                new DeltaReportSummaryItemModel
                {
                    Id = Guid.NewGuid(),
                    SocImportedCount = 11,
                    SocUnchangedCount = 5,
                    SocAdditionCount = 2,
                    SocUpdateCount = 1,
                    SocDeletionCount = 3,
                },
            };

            A.CallTo(() => fakeApiDataConnector.GetAsync<List<DeltaReportSummaryItemModel>>(A<HttpClient>.Ignored, A<Uri>.Ignored)).Returns(expectedResults);

            // act
            var results = await deltaReportApiConnector.GetSummaryAsync().ConfigureAwait(false);

            // assert
            A.CallTo(() => fakeApiDataConnector.GetAsync<List<DeltaReportSummaryItemModel>>(A<HttpClient>.Ignored, A<Uri>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.NotNull(results);
            Assert.Equal(expectedResults.Count, results!.Count);
            Assert.Equal(expectedResults.First().Id, results.First().Id);
        }

        [Fact]
        public async Task DeltaReportApiConnectorTestsGetDetailReturnsSuccess()
        {
            // arrange
            var expectedResult = new DeltaReportModel
            {
                Id = Guid.NewGuid(),
                SocImportedCount = 11,
                SocUnchangedCount = 5,
                SocAdditionCount = 2,
                SocUpdateCount = 1,
                SocDeletionCount = 3,
            };

            A.CallTo(() => fakeApiDataConnector.GetAsync<DeltaReportModel>(A<HttpClient>.Ignored, A<Uri>.Ignored)).Returns(expectedResult);

            // act
            var result = await deltaReportApiConnector.GetDetailAsync(Guid.NewGuid()).ConfigureAwait(false);

            // assert
            A.CallTo(() => fakeApiDataConnector.GetAsync<DeltaReportModel>(A<HttpClient>.Ignored, A<Uri>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Id, result?.Id);
        }
    }
}
