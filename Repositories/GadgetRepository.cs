using IbadanGadgetAPI.Data;
using IbadanGadgetAPI.Entities;
using IbadanGadgetAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IbadanGadgetAPI.Repositories
{
	public class GadgetRepository : IGadgetRepository
	{
		private readonly IbadanGadgetDbContext _db;

		public GadgetRepository(IbadanGadgetDbContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<Gadget>> GetAllAsync()
			=> await _db.Gadgets.AsNoTracking().ToListAsync();

		public async Task<Gadget?> GetByIdAsync(int id)
			=> await _db.Gadgets.FindAsync(id);

		// (Optional) Add methods for AddAsync, UpdateAsync, DeleteAsync, etc.
		public async Task<Gadget> AddAsync(Gadget gadget)
		{
			_db.Gadgets.Add(gadget);
			await _db.SaveChangesAsync();
			return gadget;
		}

		public async Task UpdateAsync(Gadget gadget)
		{
			_db.Gadgets.Update(gadget);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(Gadget gadget)
		{
			_db.Gadgets.Remove(gadget);
			await _db.SaveChangesAsync();
		}
	}
}
