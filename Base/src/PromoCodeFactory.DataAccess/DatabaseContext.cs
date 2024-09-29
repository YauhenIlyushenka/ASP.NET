using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.DataAccess
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{ }

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<PromoCode> PromoCodes { get; set; }
		public DbSet<Preference> Preferences { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Partner> Partners { get; set; }
		public DbSet<PartnerPromoCodeLimit> PartnerPromoCodeLimits { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Role>(entity =>
			{
				entity
					.HasMany(role => role.Employees)
					.WithOne(employee => employee.Role)
					.HasForeignKey(employee => employee.RoleId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("RoleId");
				entity.Property(x => x.Name).HasMaxLength(32);
				entity.Property(x => x.DescriptionRole).HasMaxLength(64);
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity
					.HasMany(employee => employee.PromoCodes)
					.WithOne(promocode => promocode.PartnerManager)
					.HasForeignKey(promocode => promocode.EmployeeId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("EmployeeId");
				entity.Property(x => x.FirstName).HasMaxLength(32);
				entity.Property(x => x.LastName).HasMaxLength(64);
				entity.Property(x => x.Email).HasMaxLength(32);
			});

			modelBuilder.Entity<PromoCode>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PromoCodeId");
				entity.Property(x => x.Code).HasMaxLength(32);
				entity.Property(x => x.ServiceInfo).HasMaxLength(64);
				entity.Property(x => x.PartnerName).HasMaxLength(32);
			});

			modelBuilder.Entity<Preference>(entity =>
			{
				entity
					.HasMany(preference => preference.Promocodes)
					.WithOne(promocode => promocode.Preference)
					.HasForeignKey(promocode => promocode.PreferenceId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PreferenceId");
				entity.Property(x => x.Name).HasMaxLength(32);
			});

			modelBuilder.SharedTypeEntity<Dictionary<string, object>>("CustomerPreference", builder =>
			{
				builder.Property<Guid>("CustomerId");
				builder.Property<Guid>("PreferenceId");
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("CustomerId");
				entity.Property(x => x.FirstName).HasMaxLength(32);
				entity.Property(x => x.LastName).HasMaxLength(64);
				entity.Property(x => x.Email).HasMaxLength(32);

				entity
					.HasMany(customer => customer.PromoCodes)
					.WithOne(promocode => promocode.Customer)
					.HasForeignKey(promocode => promocode.CustomerId);

				entity
					.HasMany(x => x.Preferences)
					.WithMany(x => x.Customers)
					.UsingEntity<Dictionary<string, object>>("CustomerPreference",
						x => x.HasOne<Preference>().WithMany().HasForeignKey("PreferenceId"),
						x => x.HasOne<Customer>().WithMany().HasForeignKey("CustomerId"),
						j => j.HasKey("CustomerId", "PreferenceId"));
			});

			modelBuilder.Entity<Partner>(entity =>
			{
				entity
					.HasMany(partner => partner.PartnerLimits)
					.WithOne(partnerLimit => partnerLimit.Partner)
					.HasForeignKey(partnerLimit => partnerLimit.PartnerId);

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
