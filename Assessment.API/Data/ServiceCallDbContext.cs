using Assessment.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assessment.API.Data
{
    public class ServiceCallDbContext : DbContext
    {
        public ServiceCallDbContext(DbContextOptions<ServiceCallDbContext> options)
            : base(options)
        {
        }

        
        // allows data manipulation from the table 'ServiceCall'

        public DbSet<ServiceCall> ServiceCalls { get; set; }
        
    }
}
