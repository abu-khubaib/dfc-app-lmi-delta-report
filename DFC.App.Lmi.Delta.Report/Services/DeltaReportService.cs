using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.App.Lmi.Delta.Report.Services
{
    public class DeltaReportService : IDeltaReportService
    {
        private readonly ILogger<DeltaReportService> logger;
        private readonly IDeltaReportApiConnector deltaReportApiConnector;
        private readonly CachedDeltaReportModel cachedDeltaReportModel;

        public DeltaReportService(ILogger<DeltaReportService> logger, IDeltaReportApiConnector deltaReportApiConnector, CachedDeltaReportModel cachedDeltaReportModel)
        {
            this.logger = logger;
            this.deltaReportApiConnector = deltaReportApiConnector;
            this.cachedDeltaReportModel = cachedDeltaReportModel;
        }

        public async Task<IList<DeltaReportSummaryItemModel>?> GetSummaryAsync()
        {
            logger.LogInformation("Retrieving summary list delta reports");

            return await deltaReportApiConnector.GetSummaryAsync().ConfigureAwait(false);
        }

        public async Task<DeltaReportModel?> GetDetailAsync(Guid id)
        {
            if (cachedDeltaReportModel?.DeltaReportModel?.Id != null && cachedDeltaReportModel.DeltaReportModel.Id.Equals(id))
            {
                logger.LogInformation($"Returning cached delta report for: {id}");

                return cachedDeltaReportModel.DeltaReportModel;
            }

            logger.LogInformation($"Retrieving delta report for: {id}");

            cachedDeltaReportModel!.DeltaReportModel = await deltaReportApiConnector.GetDetailAsync(id).ConfigureAwait(false);

            return cachedDeltaReportModel.DeltaReportModel;
        }
    }
}
