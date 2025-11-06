using IbadanGadgetAPI.Entities;

namespace IbadanGadgetAPI.Interfaces
{
	public interface IGadgetRepository
	{
		Task<IEnumerable<Gadget>> GetAllAsync();
		Task<Gadget?> GetByIdAsync(int id);
		Task<Gadget> AddAsync(Gadget gadget);
		Task UpdateAsync(Gadget gadget);
		Task DeleteAsync(Gadget gadget);
	}
}
