using IbadanGadgetAPI.Data;
using IbadanGadgetAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IbadanGadgetAPI.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class GadgetsController : ControllerBase
	{
		private readonly IbadanGadgetDbContext _context;

		public GadgetsController(IbadanGadgetDbContext context)
		{
			_context = context;
		}

		// Example: GET all gadgets
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Gadget>>> GetGadgets()
		{
			return await _context.Gadgets.ToListAsync();
		}

		// Example: GET a single gadget by ID
		[HttpGet("{id}")]
		public async Task<ActionResult<Gadget>> GetGadget(int id)
		{
			var gadget = await _context.Gadgets.FindAsync(id);
			if (gadget == null)
			{
				return NotFound();
			}
			return gadget;
		}

		// Example: POST a new gadget
		[HttpPost]
		public async Task<ActionResult<Gadget>> PostGadget(Gadget gadget)
		{
			_context.Gadgets.Add(gadget);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetGadget), new { id = gadget.Id }, gadget);
		}

		// Example: PUT (update) a gadget
		[HttpPut("{id}")]
		public async Task<IActionResult> PutGadget(int id, Gadget gadget)
		{
			if (id != gadget.Id)
			{
				return BadRequest();
			}

			_context.Entry(gadget).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Gadgets.Any(e => e.Id == id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// Example: DELETE a gadget
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGadget(int id)
		{
			var gadget = await _context.Gadgets.FindAsync(id);
			if (gadget == null)
			{
				return NotFound();
			}

			_context.Gadgets.Remove(gadget);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
