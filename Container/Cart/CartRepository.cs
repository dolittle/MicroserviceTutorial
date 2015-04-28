using System.IO;
using System.Web;
using Bifrost.Execution;
using Newtonsoft.Json;

namespace Web.Cart
{
    [Singleton]
    public class CartRepository : ICartRepository
    {
        string _path;
        object _lockObject = new object();

        public Cart Get()
        {
            lock (_lockObject)
            {
                Cart cart;
                var path = GetPath();
                if (File.Exists(path))
                {
                    var cartAsJson = File.ReadAllText(path);
                    cart = JsonConvert.DeserializeObject<Cart>(cartAsJson);
                }
                else
                {
                    cart = new Cart();
                }

                return cart;
            }
        }


        public void Update(Cart cart)
        {
            lock (_lockObject)
            {
                var path = GetPath();
                var cartAsJson = JsonConvert.SerializeObject(cart);
                File.WriteAllText(path, cartAsJson);
            }
        }

        string GetPath()
        {
            return _path;
        }


        public void Configure()
        {
            _path = HttpContext.Current.Server.MapPath("~/App_Data/Cart.json");
        }
    }
}
