using System.Collections.Generic;
using System.IO;
using System.Web;
using Bifrost.Execution;

namespace Web.Navigation
{
    [Singleton]
    public class CategoryRepository : ICategoryRepository
    {
        string _path;
        object _lockObject = new object();

        public IEnumerable<string> GetAll()
        {
            lock( _lockObject )
            {
                if( File.Exists(_path)) {
                    return File.ReadAllLines(_path);
                }

                return new string[0];
            }
        }

        public void Add(string category)
        {
            lock (_lockObject)
            {
                File.AppendAllLines(_path, new string[] { category });
            }
        }

        public void Configure()
        {
            _path = HttpContext.Current.Server.MapPath("~/App_Data/Categories.txt");
        }
    }
}
