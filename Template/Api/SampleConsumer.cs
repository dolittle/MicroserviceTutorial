using Web.Messages;
using Web.Messaging;

namespace Web.Api
{
    public class SampleConsumer : IMessageConsumer<Sample>
    {
        public void Handle(Sample message)
        {
            var dbContext = new SampleDbContext();
            var sample = dbContext.InsertSample(message.Name);
        }
    }
}
