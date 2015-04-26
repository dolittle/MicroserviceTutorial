using System.Data.Entity;
using Web.Models;

namespace Web.Api
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Sample> Sample { get; set; }

        public SampleDbContext() : base("DefaultConnection")
        {
        }

        public Sample InsertSample(string name)
        {
            var sample = new Sample { Name = name };
            Sample.Add(sample);
            SaveChanges();
            return sample;
        }
    }
}
