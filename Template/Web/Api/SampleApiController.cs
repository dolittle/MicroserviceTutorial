using System;
using System.Web.Http;
using Web.Messages;
using Web.Messaging;

namespace Web.Api
{
    [RoutePrefix("SampleApi")]
    [AllowAnonymous]
    public class SampleApiController : ApiController
    {
        IMessageBroker _messageBroker;

        public SampleApiController(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }


        [Route("Perform")]
        [HttpGet]
        public void Perform()
        {
            var name = "Something : "+Guid.NewGuid();
            _messageBroker.Send(new Sample
            {
                Name = name
            });
        }
    }
}
