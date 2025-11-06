using Microsoft.EntityFrameworkCore;
using IbadanGadgetAPI.Entities;

namespace IbadanGadgetAPI.Data
{
	public class IbadanGadgetDbContext : DbContext
	{
		public IbadanGadgetDbContext(DbContextOptions<IbadanGadgetDbContext> options)
			: base(options)
		{
		}

		public DbSet<Gadget> Gadgets { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
	