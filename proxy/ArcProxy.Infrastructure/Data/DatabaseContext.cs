using ArcProxy.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArcProxy.Infrastructure.Data
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<GeoServiceEntity> GeoServices { get; set; }
        public DbSet<GeoServiceRuleEntity> GeoServiceRules { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GeoServiceEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
                b.Property(p => p.Uri).HasMaxLength(1000);
            });

            builder.Entity<GeoServiceRuleEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.HasOne(p => p.Service)
                    .WithOne(p => p.Rule)
                    .HasForeignKey<GeoServiceRuleEntity>(p => p.ServiceId)
                    .IsRequired();
            });
        }
    }
}
