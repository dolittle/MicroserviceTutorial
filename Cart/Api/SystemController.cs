using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Web.Api
{
    public class SystemController : ApiController
    {
        private string status = "Unavailable";

        [HttpGet]
        public string Status()
        {
            return status;
        }

        [HttpPost]
        public void SetStatus(string status)
        {
            this.status = status;
        }
    }
}