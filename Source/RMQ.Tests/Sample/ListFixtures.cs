using System;
using System.Collections.Generic;


namespace RMQ.Tests.Sample
{
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
