using DFC.App.Lmi.Delta.Report.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.App.Lmi.Delta.Report.Contracts
{
    public interface IDeltaReportService
    {
        Task<IList<DeltaReportSummaryItemModel>?> GetSummaryAsync();

        Task<DeltaReportModel?> GetDetailAsync(Guid id);
    }
}
