using System.Collections.Generic;
using Bifrost.Read;

namespace Web.TestBench
{
    public class MessageType : IReadModel
    {
        public string Name { get; set; }

        public IEnumerable<string> Properties { get; set; }
    }
}
