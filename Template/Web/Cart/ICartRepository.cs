
namespace Web.Cart
{
    public interface ICartRepository
    {
        void Configure();
        Cart Get();
        void Update(Cart cart);
    }
}
