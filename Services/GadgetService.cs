using IbadanGadgetAPI.DTOs;
using IbadanGadgetAPI.Entities;
using IbadanGadgetAPI.Interfaces;

namespace IbadanGadgetAPI.Services
{
	public class GadgetService : IGadgetService
	{
		private readonly IGadgetRepository _repo;

		public GadgetService(IGadgetRepository repo) => _repo = repo;

		private static GadgetDto ToDto(Gadget g) => new GadgetDto
		{
			Id = g.Id,
			Name = g.Name,
			Description = g.Description,
			Price = g.Price,
			Quantity = g.Quantity,
			Brand = g.Brand,
			Category = g.Category,
			ImageUrl = g.ImageUrl
		};

		public async Task<IEnumerable<GadgetDto>> GetAllAsync()
		{
			var list = await _repo.GetAllAsync();
			return list.Select(ToDto);
		}

		public async Task<GadgetDto?> GetByIdAsync(int id)
		{
			var g = await _repo.GetByIdAsync(id);
			if (g == null) return null;
			return ToDto(g);
		}

		public async Task<GadgetDto> CreateAsync(GadgetDto dto)
		{
			var g = new Gadget
			{
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price,
				Quantity = (int)dto.Quantity,
				Brand = dto.Brand,
				Category = dto.Category,
				ImageUrl = dto.ImageUrl,
				CreatedAt = DateTime.UtcNow
			};
			var added = await _repo.AddAsync(g);
			return ToDto(added);
		}

		public async Task<bool> UpdateAsync(int id, GadgetDto dto)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return false;

			existing.Name = dto.Name;
			existing.Description = dto.Description;
			existing.Price = dto.Price;
			existing.Quantity = (int)dto.Quantity;
			existing.Brand = dto.Brand;
			existing.Category = dto.Category;
			existing.ImageUrl = dto.ImageUrl;
			existing.UpdatedAt = DateTime.UtcNow;

			await _repo.UpdateAsync(existing);
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return false;
			await _repo.DeleteAsync(existing);
			return true;
		}
	}
}
