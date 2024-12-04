using Microsoft.Extensions.DependencyInjection;
using Pcf.Administration.BusinessLogic.Services.Implementation;
using Pcf.Administration.BusinessLogic.Services;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.DataAccess.Repositories;

namespace Pcf.Administration.WebHost.Infrastructure
{
	public static class IocConfig
	{
		public static void AddAdministrationServices(this IServiceCollection services)
		{
			AddAdministrationLogicServices(services);
			AddDataAccessLayerRepositories(services);
		}

		private static void AddAdministrationLogicServices(this IServiceCollection services)
		{
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IEmployeeService, EmployeeService>();
		}

		private static void AddDataAccessLayerRepositories(this IServiceCollection services)
			=> services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
	}
}
