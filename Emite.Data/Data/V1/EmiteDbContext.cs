namespace Emite.Data.V1
{
    using Emite.Domain.Model.V1.AgentVM;
    using Emite.Domain.Model.V1.CallVM;
    using Emite.Domain.Model.V1.CustomerVM;
    using Emite.Domain.Model.V1.TicketVM;
    using Microsoft.EntityFrameworkCore;

    public class EmiteDbContext : DbContext
    {
        public EmiteDbContext(DbContextOptions<EmiteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
