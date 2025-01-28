using Microsoft.EntityFrameworkCore;
using Pcf.ReceivingFromPartner.Core.Domain;

namespace Pcf.ReceivingFromPartner.DataAccess
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{ }

		public DbSet<Partner> Partners { get; set; }
		public DbSet<PartnerPromoCodeLimit> PartnerPromoCodeLimits { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<PromoCode>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PromoCodeId");
				entity.Property(x => x.Code).HasMaxLength(32);
				entity.Property(x => x.ServiceInfo).HasMaxLength(64);
				entity.Property(x => x.PartnerManagerId).IsRequired(false);
			});

			modelBuilder.Entity<Partner>(entity =>
			{
				entity
					.HasMany(partner => partner.PartnerLimits)
					.WithOne(partnerLimit => partnerLimit.Partner)
					.HasForeignKey(partnerLimit => partnerLimit.PartnerId);

				entity
					.HasMany(partner => partner.PromoCodes)
					.WithOne(promocode => promocode.Partner)
					.HasForeignKey(promocode => promocode.PartnerId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PartnerId");
				entity.Property(x => x.Name).HasMaxLength(32);
			});

			modelBuilder.Entity<PartnerPromoCodeLimit>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PartnerPromocodeLimitId");
				entity.Property(x => x.CancelDate).IsRequired(false);
			});
		}
	}
}