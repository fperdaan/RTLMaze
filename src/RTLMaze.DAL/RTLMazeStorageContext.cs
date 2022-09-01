using Microsoft.EntityFrameworkCore;

using RTLMaze.DAL.Conversion;
using RTLMaze.Models;

namespace RTLMaze.DAL;

public partial class RTLMazeStorageContext : DbContext
{
	//public virtual DbSet<Cast>? Cast { get; set; }
	public virtual DbSet<Person>? Person { get; set; }
	public virtual DbSet<Title>? Title { get; set; }
	public virtual DbSet<Cast>? Cast { get; set; }
	public virtual DbSet<Job>? Job { get; set; }

	public RTLMazeStorageContext( DbContextOptions<RTLMazeStorageContext> options ) : base( options ) {}

	protected override void OnModelCreating( ModelBuilder modelBuilder )
	{
		modelBuilder.Entity<Title>( builder => 
		{
			// Disable ID auto increment
			builder.Property( p => p.ID ).ValueGeneratedNever();
		});

		modelBuilder.Entity<Person>( builder => 
		{
			// Disable ID auto increment
			builder.Property( p => p.ID ).ValueGeneratedNever();
		});
	}

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