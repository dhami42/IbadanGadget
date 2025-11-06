using IbadanGadgetAPI.DTOs;

namespace IbadanGadgetAPI.Interfaces
{
	public interface IGadgetService
	{
		Task<IEnumerable<GadgetDto>> GetAllAsync();
		Task<GadgetDto?> GetByIdAsync(int id);
		Task<GadgetDto> CreateAsync(GadgetDto dto);
		Task<bool> UpdateAsync(int id, GadgetDto dto);
		Task<bool> DeleteAsync(int id);
	}

	
}
	