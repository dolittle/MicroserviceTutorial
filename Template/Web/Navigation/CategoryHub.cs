using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace Web.Navigation
{
    public class CategoryHub : Hub
    {
        ICategoryRepository _categoryRepository;

        public CategoryHub(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<string> GetAll()
        {
            return _categoryRepository.GetAll();
        }
    }
}
