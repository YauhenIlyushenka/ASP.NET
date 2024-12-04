using Pcf.Administration.BusinessLogic.Models;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain.Administration;

namespace Pcf.Administration.BusinessLogic.Services.Implementation
{
	public class RoleService : IRoleService
	{
		private readonly IRepository<GlobalRole, int> _roleRepository;

		public RoleService(IRepository<GlobalRole, int> roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public async Task<List<RoleItemResponseDto>> GetAllAsync()
			=> (await _roleRepository.GetAllAsync()).Select(x => new RoleItemResponseDto
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToList();
	}
}
