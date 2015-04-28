using System.Collections.Generic;

namespace Web.Navigation
{
    public interface ICategoryRepository
    {
        IEnumerable<string> GetAll();

        void Add(string category);

        void Configure();
    }
}
