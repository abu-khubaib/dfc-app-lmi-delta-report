using AutoMapper;
using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.App.Lmi.Delta.Report.Controllers
{
    public class DeltaReportController : Controller
    {
        private readonly ILogger<DeltaReportController> logger;
        private readonly IMapper mapper;
        private readonly IDeltaReportService deltaReportService;

        public DeltaReportController(ILogger<DeltaReportController> logger, IMapper mapper, IDeltaReportService deltaReportService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.deltaReportService = deltaReportService;
        }

        // GET: DeltaReport
        public async Task<ActionResult> Index()
        {
            var deltaReports = await deltaReportService.GetSummaryAsync().ConfigureAwait(false);
            var viewModels = mapper.Map<List<DeltaReportSummaryItemViewModel>>(deltaReports);

            logger.LogInformation($"Retrieved {viewModels.Count} delta reports");
            return View(viewModels);
        }

        // GET: DeltaReport/SocIndex/guid
        public async Task<ActionResult> SocIndex(Guid id)
        {
            var deltaReport = await deltaReportService.GetDetailAsync(id).ConfigureAwait(false);

            if (deltaReport != null)
            {
                var viewModel = mapper.Map<DeltaReportViewModel>(deltaReport);

                logger.LogInformation($"Retrieved Delta report for: {id}");
                return View(viewModel);
            }

            logger.LogWarning($"Delta report not found for: {id}");
            return NotFound();
        }

        // GET: DeltaReport/Details/guid/soc
        public async Task<ActionResult> Details(Guid id, int soc)
        {
            var deltaReport = await deltaReportService.GetDetailAsync(id).ConfigureAwait(false);

            if (deltaReport != null)
            {
                var deltaReportSoc = deltaReport.DeltaReportSocs.FirstOrDefault(f => f.Soc == soc);
                var viewModel = mapper.Map<DeltaReportSocViewModel>(deltaReportSoc);

                viewModel.DeltaReportId = deltaReport.Id;
                viewModel.ReportTitle = deltaReport.CreatedDate.ToString("O");

                logger.LogInformation($"Retrieved Delta report for: {id} / {soc}");
                return View(viewModel);
            }

            logger.LogWarning($"Delta report not found for: {id} / {soc}");
            return NotFound();
        }
    }
}
