using System.Web.Mvc;
using Web.Api;
using Web.Models;

namespace Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var api = new CartController();
            var cartItems = api.Cart();

            return View(cartItems); //new CartItem(){MaterialNumber = "11111"});
        }
    }
}