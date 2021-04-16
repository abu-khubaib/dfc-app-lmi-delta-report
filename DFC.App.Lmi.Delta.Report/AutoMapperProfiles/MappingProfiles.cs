using AutoMapper;
using DFC.App.Lmi.Delta.Report.Models;
using DFC.App.Lmi.Delta.Report.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report.AutoMapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DeltaReportSummaryItemModel, DeltaReportSummaryItemViewModel>()
                .ForMember(d => d.ReportTitle, s => s.MapFrom(a => a.CreatedDate.ToString("O")));

            CreateMap<DeltaReportModel, DeltaReportViewModel>()
                .ForMember(d => d.ReportTitle, s => s.MapFrom(a => a.CreatedDate.ToString("O")));

            CreateMap<DeltaReportSocModel, DeltaReportSocViewModel>()
                .ForMember(d => d.DeltaReportId, s => s.Ignore())
                .ForMember(d => d.ReportTitle, s => s.Ignore());
        }
    }
}