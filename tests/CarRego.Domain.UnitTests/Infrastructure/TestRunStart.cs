using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework("EFCore.Domain.Tests.Infrastructure.TestRunStart", "CarRego.Domain.Tests")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace CarRego.Domain.UnitTests.Infrastructure;

public class TestRunStart : XunitTestFramework
{
    public TestRunStart(IMessageSink messageSink) : base(messageSink)
    {
        TestHelper.GetMigrationContext().Database.Migrate();
    }
}