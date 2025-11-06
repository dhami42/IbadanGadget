using IbadanGadgetAPI.DTOs;
using IbadanGadgetAPI.Entities;

namespace IbadanGadgetAPI.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetAllAsync();
		Task<UserDto?> GetByIdAsync(int id);
		Task<UserDto> CreateAsync(UserDto dto);
		Task<bool> UpdateAsync(int id, UserDto dto);
		Task<bool> DeleteAsync(int id);
		Task CreateAsync(User entity);
		Task UpdateAsync(UserDto existing);
	}
}
