using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.Models;
using DFC.App.Lmi.Delta.Report.Models.ClientOptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DFC.App.Lmi.Delta.Report.Connectors
{
    public class DeltaReportApiConnector : IDeltaReportApiConnector
    {
        private readonly ILogger<DeltaReportApiConnector> logger;
        private readonly HttpClient httpClient;
        private readonly IApiDataConnector apiDataConnector;
        private readonly DeltaReportApiClientOptions deltaReportApiClientOptions;

        public DeltaReportApiConnector(
            ILogger<DeltaReportApiConnector> logger,
            HttpClient httpClient,
            IApiDataConnector apiDataConnector,
            DeltaReportApiClientOptions deltaReportApiClientOptions)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.apiDataConnector = apiDataConnector;
            this.deltaReportApiClientOptions = deltaReportApiClientOptions;
        }

        public async Task<IList<DeltaReportSummaryItemModel>?> GetSummaryAsync()
        {
            var url = new Uri($"{deltaReportApiClientOptions.BaseAddress}", UriKind.Absolute);
            logger.LogInformation($"Retrieving summaries from LMI delta report API: {url}");
            return await apiDataConnector.GetAsync<List<DeltaReportSummaryItemModel>>(httpClient, url).ConfigureAwait(false);
        }

        public async Task<DeltaReportModel?> GetDetailAsync(Guid socId)
        {
            var url = new Uri($"{deltaReportApiClientOptions.BaseAddress}{socId}", UriKind.Absolute);
            logger.LogInformation($"Retrieving details from LMI delta report API: {url}");
            return await apiDataConnector.GetAsync<DeltaReportModel>(httpClient, url).ConfigureAwait(false);
        }
    }
}
