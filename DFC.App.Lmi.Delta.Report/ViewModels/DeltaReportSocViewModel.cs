using DFC.App.Lmi.Delta.Report.Common;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class DeltaReportSocViewModel
    {
        public Guid? DeltaReportId { get; set; }

        public string? ReportTitle { get; set; }

        public int Soc { get; set; }

        public string? SocTitle { get; set; }

        public DeltaReportState State { get; set; }

        public string? DraftJobGroup { get; set; }

        public string? PublishedJobGroup { get; set; }
    }
}
