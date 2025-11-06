using IbadanGadgetAPI.Data;
using IbadanGadgetAPI.Entities;
using IbadanGadgetAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IbadanGadgetAPI.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IbadanGadgetDbContext _db;
		public UserRepository(IbadanGadgetDbContext db) => _db = db;

		public async Task<IEnumerable<User>> GetAllAsync()
			=> await _db.Users.AsNoTracking().ToListAsync();

		public async Task<User?> GetByIdAsync(int id)
			=> await _db.Users.FindAsync(id);

		public async Task<User?> GetByEmailAsync(string email)
			=> await _db.Users.AsNoTracking()
							  .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

		public async Task<User> AddAsync(User user)
		{
			_db.Users.Add(user);
			await _db.SaveChangesAsync();
			return user;
		}

		public async Task UpdateAsync(User user)
		{
			_db.Users.Update(user);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(User user)
		{
			_db.Users.Remove(user);
			await _db.SaveChangesAsync();
		}
	}
}
