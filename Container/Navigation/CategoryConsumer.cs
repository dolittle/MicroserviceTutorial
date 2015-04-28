using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Web.Messages;
using Web.Messaging;

namespace Web.Navigation
{
    public class CategoryConsumer : IMessageConsumer<Message>
    {
        ICategoryRepository _categoryRepository;
        public CategoryConsumer(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Handle(Message message)
        {
            if (message is MaterialAdded) AddIfCategoryDoesNotExist(((MaterialAdded)message).Category);
            if (message is CategoryAdded) AddIfCategoryDoesNotExist(((CategoryAdded)message).Category);
        }

        void AddIfCategoryDoesNotExist(string category)
        {
            var categories = new List<string>(_categoryRepository.GetAll());

            if (!categories.Any(t => t.ToLowerInvariant() == category))
            {
                _categoryRepository.Add(category);
                categories.Add(category);
            }
            
            var hub = GlobalHost.ConnectionManager.GetHubContext<CategoryHub>();
            hub.Clients.All.categoriesChanged(categories);
        }
    }
}
