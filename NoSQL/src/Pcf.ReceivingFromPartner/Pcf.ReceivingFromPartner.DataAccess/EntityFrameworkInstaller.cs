using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Pcf.ReceivingFromPartner.DataAccess
{
	public static class EntityFrameworkInstaller
	{
		public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
			=> services.AddDbContext<DataContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
	}
}
