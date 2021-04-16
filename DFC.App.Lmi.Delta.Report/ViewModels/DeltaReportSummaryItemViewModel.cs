using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class DeltaReportSummaryItemViewModel
    {
        public Guid? Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ReportTitle { get; set; }

        public int SocImportedCount { get; set; }

        public int SocUnchangedCount { get; set; }

        public int SocAdditionCount { get; set; }

        public int SocUpdateCount { get; set; }

        public int SocDeletionCount { get; set; }
    }
}
