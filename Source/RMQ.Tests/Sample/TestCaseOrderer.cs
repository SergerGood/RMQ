using System;
using System.Collections.Generic;
using System.Linq;

using Xunit.Abstractions;
using Xunit.Sdk;


namespace RMQ.Tests.Sample
{
    public class TestCaseOrderer : ITestCaseOrderer
    {
        private readonly IMessageSink diagnosticMessageSink;


        public TestCaseOrderer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }


        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            var result = testCases.ToList();
            var message = new DiagnosticMessage("Ordered {0} test cases", result.Count);
            diagnosticMessageSink.OnMessage(message);
            return result;
        }
    }
}
