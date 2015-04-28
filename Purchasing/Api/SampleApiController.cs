using System;
using System.Web.Http;
using Web.Messages;
using Web.Messaging;

namespace Web.Api
{
    public class SampleApiController : ApiController
    {
        IMessageBroker _messageBroker;

        public SampleApiController(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        [HttpGet]
        public string Perform(int blah)
        {
            var name = "Something : "+Guid.NewGuid();
            _messageBroker.Send(new Sample
            {
                Name = name
            });

            return "Hello";
        }
    }
}
