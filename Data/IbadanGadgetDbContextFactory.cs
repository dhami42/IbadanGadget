using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace IbadanGadgetAPI.Data
{
	public class IbadanGadgetDbContextFactory : IDesignTimeDbContextFactory<IbadanGadgetDbContext>
	{
		public IbadanGadgetDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<IbadanGadgetDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			optionsBuilder.UseNpgsql	(connectionString);

			return new IbadanGadgetDbContext(optionsBuilder.Options);
		}
	}
}
