using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IbadanGadgetAPI.DTOs;
using IbadanGadgetAPI.Entities;
using IbadanGadgetAPI.Services;
using IbadanGadgetAPI.Interfaces;

namespace IbadanGadgetAPI.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _service;

		public UsersController(IUserService service)
		{
			_service = service;
		}

		// GET api/users/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var user = await _service.GetByIdAsync(id);
			if (user == null)
				return NotFound();

			return Ok(ToDto(user)); 
		}

		private object? ToDto(UserDto user)
		{
			throw new NotImplementedException();
		}

		// POST api/users
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] UserDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var entity = new User
			{
				Email = dto.Email,
				FullName = dto.FullName,
				PasswordHash = "PLACEHOLDER_HASH"
			};

			await _service.CreateAsync(entity);
			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, ToDto(entity));


			
		}

		// PUT api/users/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
		{
			if (id != dto.Id)
				return BadRequest("ID mismatch");

			var existing = await _service.GetByIdAsync(id);
			if (existing == null)
				return NotFound();

			existing.Email = dto.Email;
			existing.FullName = dto.FullName;

			await _service.UpdateAsync(id, existing); // ✅ Pass both id and dto

			return NoContent();
		}

		// DELETE api/users/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var existing = await _service.GetByIdAsync(id);
			if (existing == null)
				return NotFound();

			await _service.DeleteAsync(id);
			return NoContent();
		}

		// Help
		private static UserDto ToDto(User user)
		{
			return new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				FullName = user.FullName
			};
		}

	}
}