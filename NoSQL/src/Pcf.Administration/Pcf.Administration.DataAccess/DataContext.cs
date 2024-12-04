using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Pcf.Administration.Core.Domain.Administration;

namespace Pcf.Administration.DataAccess
{
	public class DataContext : DbContext
	{
		public DbSet<GlobalRole> Roles { get; set; }
		public DbSet<Employee> Employees { get; set; }

		public DataContext()
		{ }

		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{ 
			/*
				Disable because MongoDb Provider for EF Core mantains transactions with DB Replica Set.
				Due to I have one server with MongoDb on my laptop, this one has been disabled to work properly.
			*/
			Database.AutoTransactionBehavior = AutoTransactionBehavior.Never; 
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Employee>().ToCollection("employees");
			modelBuilder.Entity<GlobalRole>().ToCollection("roles");

			modelBuilder.Entity<Employee>().OwnsOne(e => e.Role); // Configure nested document for MongoDb
		}
	}
}