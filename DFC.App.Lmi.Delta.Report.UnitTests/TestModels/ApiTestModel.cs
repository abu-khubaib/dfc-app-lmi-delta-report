using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report.UnitTests.TestModels
{
    [ExcludeFromCodeCoverage]
    public class ApiTestModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}
