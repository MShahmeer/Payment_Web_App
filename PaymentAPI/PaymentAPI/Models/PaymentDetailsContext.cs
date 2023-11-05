using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Models
{
    public class PaymentDetailsContext : DbContext
    {
        public PaymentDetailsContext(DbContextOptions options) : base(options)
        {
        }

        //We Define PaymentDetails of type DbSet<PaymentDetails>. In EntityFrameworkCore the DbSet represents the collection of entities in this case PaymentDetails entites that can be queried and manipulated as if they were the database tables. Now we can use PaymentDetails to query the database.
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
