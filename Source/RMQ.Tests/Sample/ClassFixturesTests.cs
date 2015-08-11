using System;
using System.Collections.Generic;

using Xunit;


namespace RMQ.Tests.Sample
{
    public class ClassFixturesTests : IClassFixture<ListFixtures>
    {
        readonly ListFixtures fixture;

        public ClassFixturesTests(ListFixtures fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void CheckTotalCountAfterAdd1()
        {
            fixture.Collection.AddRange(new[] { 4, 5 });
            var count = fixture.Collection.Count;
            Assert.Equal(5, count);
        }

        [Fact]
        public void CheckTotalCountAfterAdd2()
        {
            fixture.Collection.AddRange(new[] { 4, 5 });
            var count = fixture.Collection.Count;

            Assert.Equal(5, count);
        }
    }


    public class ListFixtures : IDisposable
    {
        public ListFixtures()
        {
            Collection = new List<int> { 1, 2, 3 };
        }

        public List<int> Collection { get; }

        public void Dispose()
        {
            Collection.Clear();
        }
    }
}
