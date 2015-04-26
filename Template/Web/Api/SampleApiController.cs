using System.Web.Http;
using Web.Messaging;

namespace Web.Api
{
    [RoutePrefix("SampleApi")]
    [AllowAnonymous]
    public class SampleApiController : ApiController
    {
        public SampleApiController(IMessageBroker messageBroker)
        {
            var i = 0;
            i++;
        }


        [Route("Alive")]
        [HttpGet]
        public void Alive()
        {
            //return true;
        }
    }
}
