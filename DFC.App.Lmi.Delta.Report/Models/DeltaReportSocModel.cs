using DFC.App.Lmi.Delta.Report.Common;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report.Models
{
    [ExcludeFromCodeCoverage]
    public class DeltaReportSocModel
    {
        public int Soc { get; set; }

        public string? SocTitle { get; set; }

        public DeltaReportState State { get; set; }

        public string? Delta { get; set; }

        public string? DraftJobGroup { get; set; }

        public string? PublishedJobGroup { get; set; }
    }
}
