using Microsoft.Extensions.DependencyInjection;
using RTLMaze.Models;

namespace RTLMaze.DAL;

static public class Configure
{
	public static void ConfigureServices( IServiceCollection services  )
	{
		services
			.AddScoped<IRepository<Title>, AtomicRepository<Title>>();
		// 	.AddScoped<IRepository<Teacher>, AtomicRepository<Teacher>>()
		// 	.AddScoped<IRepository<Person>, AtomicRepository<Person>>();
	}
}