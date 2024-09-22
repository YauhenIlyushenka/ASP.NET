using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;

namespace PromoCodeFactory.DataAccess
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<PromoCode> PromoCodes { get; set; }
		public DbSet<Preference> Preferences { get; set; }
		public DbSet<Customer> Customers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Role>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("RoleId");
				entity.Property(x => x.Name).HasMaxLength(32);
				entity.Property(x => x.Description).HasMaxLength(64);

				entity.HasData(FakeDataFactory.Roles);
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasOne(employee => employee.Role)
					.WithOne(role => role.Employee)
					.HasForeignKey<Role>(role => role.EmployeeId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("EmployeeId");
				entity.Property(x => x.FirstName).HasMaxLength(32);
				entity.Property(x => x.LastName).HasMaxLength(64);
				entity.Property(x => x.Email).HasMaxLength(32);

				entity.HasData(FakeDataFactory.Employees);
			});

			modelBuilder.Entity<PromoCode>(entity =>
			{
				entity.HasOne(promocode => promocode.PartnerManager)
					.WithOne(employee => employee.PromoCode)
					.HasForeignKey<Employee>(employee => employee.PromoCodeId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PromoCodeId");
				entity.Property(x => x.Code).HasMaxLength(32);
				entity.Property(x => x.ServiceInfo).HasMaxLength(64);
				entity.Property(x => x.PartnerName).HasMaxLength(32);
			});

			modelBuilder.Entity<Preference>(entity =>
			{
				entity.HasOne(preference => preference.PromoCode)
					.WithOne(promocode => promocode.Preference)
					.HasForeignKey<Preference>(preference => preference.PromoCodeId);

				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("PreferenceId");
				entity.Property(x => x.Name).HasMaxLength(32);

				entity.HasData(FakeDataFactory.Preferences);
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasColumnName("CustomerId");
				entity.Property(x => x.FirstName).HasMaxLength(32);
				entity.Property(x => x.LastName).HasMaxLength(64);
				entity.Property(x => x.Email).HasMaxLength(32);

				entity.HasMany(customer => customer.PromoCodes)
					.WithOne(promocode => promocode.Customer)
					.HasForeignKey(promocode => promocode.CustomerId);

				entity.HasMany(customer => customer.Preferences)
					.WithMany(preference => preference.Customers)
					.UsingEntity(
					   "CustomerPreference",
					   l => l.HasOne(typeof(Customer)).WithMany().HasForeignKey("CustomerId"),
					   r => r.HasOne(typeof(Preference)).WithMany().HasForeignKey("PreferenceId"),
					   j => j.HasKey("CustomerId", "PreferenceId"));

				entity.HasData(FakeDataFactory.Customers);
			});
		}
	}
}
