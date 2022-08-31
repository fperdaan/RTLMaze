using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RTLMaze.DAL.Conversion;
using RTLMaze.Models;

namespace RTLMaze.DAL;

public partial class RTLMazeStorageContext : DbContext
{
	//public virtual DbSet<Cast>? Cast { get; set; }
	public virtual DbSet<Person>? Person { get; set; }
	public virtual DbSet<Title>? Title { get; set; }

	public RTLMazeStorageContext( DbContextOptions<RTLMazeStorageContext> options ) : base( options ) {}

	// protected override void OnModelCreating( ModelBuilder builder )
	// {
	// }

	protected override void ConfigureConventions( ModelConfigurationBuilder configurationBuilder )
	{
		configurationBuilder
        	.Properties<IDictionary<string, string>>()
        	.HaveConversion<SerializerConversion<IDictionary<string,string>, Dictionary<string,string>>, SerializerComparer<IDictionary<string,string>>>();
			

		configurationBuilder
        	.Properties<ICollection<string>>()
        	.HaveConversion<StringCollectionConversion, StringCollectionComparer>();
	}
}