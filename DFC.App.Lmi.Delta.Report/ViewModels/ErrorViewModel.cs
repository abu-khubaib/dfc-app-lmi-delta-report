using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
