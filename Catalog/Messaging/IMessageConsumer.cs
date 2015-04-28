
namespace Web.Messaging
{
    public interface IMessageConsumer<T>
    {
        void Handle(T message);
    }
}
