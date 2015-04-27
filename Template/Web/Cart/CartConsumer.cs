using System;
using Microsoft.AspNet.SignalR;
using Web.Messages;
using Web.Messaging;

namespace Web.Cart
{
    public class CartConsumer : IMessageConsumer<Message>
    {
        ICartRepository _cartRepository;

        public CartConsumer(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void Handle(Message message)
        {
            if (message is ItemAddedToCart) Added(message as ItemAddedToCart);
            if (message is ItemRemovedFromCart) Removed(message as ItemRemovedFromCart);
        }

        void Added(ItemAddedToCart message)
        {
            var cart = _cartRepository.Get();
            cart.Items += message.Quantity;
            cart.Total += message.Price * (decimal)message.Quantity;
            _cartRepository.Update(cart);

            CartChanged(cart);
        }

        void Removed(ItemRemovedFromCart message)
        {
            var cart = _cartRepository.Get();
            cart.Items -= message.Quantity;
            cart.Total -= message.Price * (decimal)message.Quantity;
            _cartRepository.Update(cart);

            CartChanged(cart);
        }

        void CartChanged(Cart cart)
        {
            var cartHubContext = GlobalHost.ConnectionManager.GetHubContext<CartHub>();
            cartHubContext.Clients.All.cartChanged(cart);
        }
    }
}
