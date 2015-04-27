using Microsoft.AspNet.SignalR;

namespace Web.Cart
{
    public class CartHub : Hub
    {
        ICartRepository _cartRepository;

        public CartHub(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Cart GetCart()
        {
            return _cartRepository.Get();
        }
        
    }
}
