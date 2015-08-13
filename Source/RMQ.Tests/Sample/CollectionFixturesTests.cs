using System;

using Xunit;


namespace RMQ.Tests.Sample
{
    public class CollectionFixturesTests
    {
        [CollectionDefinition("List collection")]
        public class DatabaseCollection : ICollectionFixture<ListFixtures>
        {
        }

        [Collection("List collection")]
        public class ListTestClass1
        {
            readonly ListFixtures fixture;

            public ListTestClass1(ListFixtures fixture)
            {
                this.fixture = fixture;
            }

            [Fact]
            public void ListCollectionFixtureTest1()
            {
                fixture.Collection.AddRange(new[] { 4, 5 });
                var count = fixture.Collection.Count;
                Assert.Equal(5, count);
            }
        }

        [Collection("List collection")]
        public class ListTestClass2
        {
            readonly ListFixtures fixture;

            public ListTestClass2(ListFixtures fixture)
            {
                this.fixture = fixture;
            }

            [Fact]
            public void ListCollectionFixtureTest2()
            {
                fixture.Collection.AddRange(new[] { 4, 5 });
                var count = fixture.Collection.Count;

                Assert.Equal(5, count);
            }
        }
    }
}
