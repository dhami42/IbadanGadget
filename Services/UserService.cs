using IbadanGadgetAPI.DTOs;
using IbadanGadgetAPI.Entities;
using IbadanGadgetAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IbadanGadgetAPI.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repo;
		private readonly IPasswordHasher<User> _hasher;

		public UserService(IUserRepository repo, IPasswordHasher<User> hasher)
		{
			_repo = repo;
			_hasher = hasher;
		}

		private static UserDto ToDto(User u) => new UserDto
		{
			Id = u.Id,
			FullName = u.FullName,
			Email = u.Email
		};

		public async Task<IEnumerable<UserDto>> GetAllAsync()
		{
			var users = await _repo.GetAllAsync();
			return users.Select(ToDto);
		}

		public async Task<UserDto?> GetByIdAsync(int id)
		{
			var u = await _repo.GetByIdAsync(id);
			return u == null ? null : ToDto(u);
		}

		public async Task<UserDto?> CreateAsync(UserDto dto)
		{
			var existing = await _repo.GetByEmailAsync(dto.Email);
			if (existing != null) return null;

			var user = new User
			{
				FullName = dto.FullName,
				Email = dto.Email,
				CreatedAt = DateTime.UtcNow
			};

			user.PasswordHash = _hasher.HashPassword(user, dto.Password);

			var added = await _repo.AddAsync(user);
			return ToDto(added);
		}

		public async Task<bool> UpdateAsync(int id, UserDto dto)
		{
			var user = await _repo.GetByIdAsync(id);
			if (user == null) return false;

			user.FullName = dto.FullName;
			user.Email = dto.Email;

			if (!string.IsNullOrWhiteSpace(dto.Password))
				user.PasswordHash = _hasher.HashPassword(user, dto.Password);

			await _repo.UpdateAsync(user);
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return false;
			await _repo.DeleteAsync(existing);
			return true;
		}

		// ✅ Extra method to satisfy interface
		public async Task CreateAsync(User entity)
		{
			// just call repository AddAsync directly
			await _repo.AddAsync(entity);
		}

		// ✅ Extra method to satisfy interface
		public async Task UpdateAsync(UserDto existing)
		{
			var user = await _repo.GetByIdAsync(existing.Id);
			if (user == null) return;

			user.FullName = existing.FullName;
			user.Email = existing.Email;

			if (!string.IsNullOrWhiteSpace(existing.Password))
				user.PasswordHash = _hasher.HashPassword(user, existing.Password);

			await _repo.UpdateAsync(user);
		}
	}
}
