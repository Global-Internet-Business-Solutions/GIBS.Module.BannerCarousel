using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace GIBS.Module.BannerCarousel.Repository
{
    public class BannerCarouselContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.BannerCarousel> BannerCarousel { get; set; }

        public BannerCarouselContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.BannerCarousel>().ToTable(ActiveDatabase.RewriteName("GIBSBannerCarousel"));
        }
    }
}
