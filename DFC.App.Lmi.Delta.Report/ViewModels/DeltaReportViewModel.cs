using DFC.App.Lmi.Delta.Report.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DFC.App.Lmi.Delta.Report.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class DeltaReportViewModel
    {
        public Guid? Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ReportTitle { get; set; }

        public List<DeltaReportSocViewModel>? DeltaReportSocs { get; set; }

        public int SocImportedCount { get; set; }

        public int SocUnchangedCount { get; set; }

        public int SocAdditionCount { get; set; }

        public int SocUpdateCount { get; set; }

        public int SocDeletionCount { get; set; }

        public List<DeltaReportSocViewModel> DeltaReportSocsForState(DeltaReportState deltaReportState)
        {
            if (DeltaReportSocs == null)
            {
                return new List<DeltaReportSocViewModel>();
            }

            return (from a in DeltaReportSocs where a.State == deltaReportState select a).ToList();
        }
    }
}
