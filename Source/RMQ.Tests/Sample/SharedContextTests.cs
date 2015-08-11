using System;
using System.Collections.Generic;

using Xunit;


namespace RMQ.Tests.Sample
{
    public class SharedContextTests : IDisposable
    {
        private readonly List<int> collection;

        public SharedContextTests()
        {
            collection = new List<int> { 1, 2, 3 };
        }

        public void Dispose()
        {
            collection.Clear();
        }

        [Fact]
        public void CheckCount()
        {
            var count = collection.Count;
            Assert.Equal(3, count);
        }

        [Fact]
        public void CheckCountAfterAdd()
        {
            collection.AddRange(new [] { 4, 5 });
            var count = collection.Count;

            Assert.Equal(5, count);
        }
    }
}
