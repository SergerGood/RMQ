using Xunit;
using Xunit.Abstractions;


namespace RMQ.Tests
{
    [TestCaseOrderer("RMQ.Tests.Sample.TestCaseOrderer", "RMQ.Tests")]
    public class SimpleTests
    {
        private readonly ITestOutputHelper output;

        public SimpleTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PassingTest()
        {
            output.WriteLine("This is output text!");
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}