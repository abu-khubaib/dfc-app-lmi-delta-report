using AutoMapper;
using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.Controllers;
using DFC.App.Lmi.Delta.Report.Models;
using DFC.App.Lmi.Delta.Report.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.Lmi.Delta.Report.UnitTests.ControllerTests
{
    [Trait("Category", "Delta Report controller Unit Tests")]
    public class DeltaReportControllerTests
    {
        private readonly ILogger<DeltaReportController> fakeLogger = A.Fake<ILogger<DeltaReportController>>();
        private readonly IMapper fakeMapper = A.Fake<IMapper>();
        private readonly IDeltaReportService fakeDeltaReportService = A.Fake<IDeltaReportService>();

        [Fact]
        public async Task DeltaReportControllerIndexHtmlReturnsSuccess()
        {
            // Arrange
            const int resultsCount = 2;
            var expectedResults = A.CollectionOfFake<DeltaReportSummaryItemModel>(resultsCount);
            using var controller = BuildPagesController();

            A.CallTo(() => fakeDeltaReportService.GetSummaryAsync()).Returns(expectedResults);
            A.CallTo(() => fakeMapper.Map<List<DeltaReportSummaryItemViewModel>>(A<List<DeltaReportSummaryItemModel>>.Ignored)).Returns(A.Fake<List<DeltaReportSummaryItemViewModel>>());

            // Act
            var result = await controller.Index().ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportService.GetSummaryAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<List<DeltaReportSummaryItemViewModel>>(A<List<DeltaReportSummaryItemModel>>.Ignored)).MustHaveHappenedOnceExactly();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModels = Assert.IsAssignableFrom<List<DeltaReportSummaryItemViewModel>>(viewResult.ViewData.Model);

            A.Equals(resultsCount, viewModels!.Count);
        }

        [Fact]
        public async Task DeltaReportControllerSocIndexHtmlReturnsSuccess()
        {
            // Arrange
            var expectedResult = A.Fake<DeltaReportModel>();
            using var controller = BuildPagesController();
            var expectedViewModel = new DeltaReportViewModel
            {
                Id = Guid.NewGuid(),
                SocImportedCount = 11,
                SocUnchangedCount = 5,
                SocAdditionCount = 2,
                SocUpdateCount = 1,
                SocDeletionCount = 3,
            };

            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).Returns(expectedResult);
            A.CallTo(() => fakeMapper.Map<DeltaReportViewModel>(A<DeltaReportModel>.Ignored)).Returns(expectedViewModel);

            // Act
            var result = await controller.SocIndex(expectedViewModel.Id.Value).ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<DeltaReportViewModel>(A<DeltaReportModel>.Ignored)).MustHaveHappenedOnceExactly();

            var viewResult = Assert.IsType<ViewResult>(result);
            _ = Assert.IsAssignableFrom<DeltaReportViewModel>(viewResult.ViewData.Model);
            var viewModel = viewResult.ViewData.Model as DeltaReportViewModel;
            Assert.Equal(expectedViewModel, viewModel);
        }

        [Fact]
        public async Task DeltaReportControllerSocIndexHtmlReturnsNotFound()
        {
            // Arrange
            const HttpStatusCode expectedStatusCode = HttpStatusCode.NotFound;
            DeltaReportModel? nullResult = null;
            using var controller = BuildPagesController();

            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).Returns(nullResult);

            // Act
            var result = await controller.SocIndex(Guid.NewGuid()).ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<DeltaReportViewModel>(A<DeltaReportModel>.Ignored)).MustNotHaveHappened();

            var statusCodeResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(expectedStatusCode, (HttpStatusCode)statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeltaReportControllerDetailsHtmlReturnsSuccess()
        {
            // Arrange
            var expectedResult = A.Fake<DeltaReportModel>();
            using var controller = BuildPagesController();
            var expectedViewModel = new DeltaReportSocViewModel
            {
                DeltaReportId = Guid.NewGuid(),
                ReportTitle = "A report title",
                Soc = 1234,
                SocTitle = "A SIC title",
            };
            expectedResult.DeltaReportSocs = new List<DeltaReportSocModel> { new DeltaReportSocModel { Soc = expectedViewModel.Soc } };

            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).Returns(expectedResult);
            A.CallTo(() => fakeMapper.Map<DeltaReportSocViewModel>(A<DeltaReportSocModel>.Ignored)).Returns(expectedViewModel);

            // Act
            var result = await controller.Details(expectedViewModel.DeltaReportId.Value, expectedViewModel.Soc).ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<DeltaReportSocViewModel>(A<DeltaReportSocModel>.Ignored)).MustHaveHappenedOnceExactly();

            var viewResult = Assert.IsType<ViewResult>(result);
            _ = Assert.IsAssignableFrom<DeltaReportSocViewModel>(viewResult.ViewData.Model);
            var viewModel = viewResult.ViewData.Model as DeltaReportSocViewModel;
            Assert.Equal(expectedViewModel, viewModel);
        }

        [Fact]
        public async Task DeltaReportControllerDetailsHtmlReturnsNotFound()
        {
            // Arrange
            const HttpStatusCode expectedStatusCode = HttpStatusCode.NotFound;
            DeltaReportModel? nullResult = null;
            using var controller = BuildPagesController();

            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).Returns(nullResult);

            // Act
            var result = await controller.Details(Guid.NewGuid(), 1234).ConfigureAwait(false);

            // Assert
            A.CallTo(() => fakeDeltaReportService.GetDetailAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<DeltaReportSocViewModel>(A<DeltaReportSocModel>.Ignored)).MustNotHaveHappened();

            var statusCodeResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(expectedStatusCode, (HttpStatusCode)statusCodeResult.StatusCode);
        }

        private DeltaReportController BuildPagesController()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = MediaTypeNames.Text.Html;

            var controller = new DeltaReportController(fakeLogger, fakeMapper, fakeDeltaReportService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                },
            };

            return controller;
        }
    }
}
