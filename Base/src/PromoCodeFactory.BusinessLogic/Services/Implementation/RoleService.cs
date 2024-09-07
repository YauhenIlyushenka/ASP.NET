using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class RoleService : IRoleService
	{
		private readonly IRepository<Role> _roleRepository;

		public RoleService(IRepository<Role> rolesRepository)
		{
			_roleRepository = rolesRepository;
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
