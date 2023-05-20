using Duende.IdentityServer.EntityFramework.Options;
using MessengerApp.Server.Entyties;
using MessengerApp.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MessengerApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Chat>().HasMany(e=>e.Users).WithOne().HasPrincipalKey(e=>e.Id);
            builder.Entity<Message>().HasOne(e => e.Sender).WithOne().HasPrincipalKey<Message>(e=>e.Id);
            base.OnModelCreating(builder);
        }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}