using Bifrost.Configuration;
using System;
using System.Web.Mvc;
using Web.Api;
using Web.Messaging;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewCatalog()
        {
            var dbContext = new ProductDbContext();
           
            return View(dbContext.GetByCategory("IT"));
        }

        public ActionResult AddToCart(string id, decimal price)
        {
            var messageBroker = Configure.Instance.Container.Get<IMessageBroker>();
            CatalogApiController capi = new CatalogApiController(messageBroker);

            capi.AddToCart(id, price);

            var dbContext = new ProductDbContext();

            return View(dbContext.GetByCategory("IT"));
}
    }
}