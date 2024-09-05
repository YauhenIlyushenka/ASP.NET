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

		public async Task<RoleItemResponseDto?> GetByIdAsync(Guid id)
		{
			var role = await _roleRepository.GetByIdAsync(id);

			return role != null 
				? new RoleItemResponseDto
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description
				}
				: null;
		}

		public async Task CreateAsync(RoleItemRequestDto model)
		{
			await _roleRepository.CreateAsync(new Role
			{
				Id = Guid.NewGuid(),
				Name = model.Name,
				Description = model.Description
			});
		}

		public async Task UpdateAsync(RoleItemRequestDto model)
		{
			var role = await _roleRepository.GetByIdAsync(model.Id);

			role.Name = model.Name;
			role.Description = model.Description;
			
		}

		public async Task DeleteAsync(Guid id)
		{
			var role = await _roleRepository.GetByIdAsync(id);
			await _roleRepository.DeleteAsync(role);
		}
	}
}
