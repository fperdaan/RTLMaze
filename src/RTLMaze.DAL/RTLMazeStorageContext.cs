using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RTLMaze.DAL.Conversion;
using RTLMaze.Models;

namespace RTLMaze.DAL;

public partial class RTLMazeStorageContext : DbContext
{
	//public virtual DbSet<Cast>? Cast { get; set; }
	public virtual DbSet<Title>? Title { get; set; }

	public RTLMazeStorageContext( DbContextOptions<RTLMazeStorageContext> options ) : base( options ) {}

	protected override void OnModelCreating( ModelBuilder builder )
	{
		// -- Seed data
		builder.Entity<Title>().HasData( 	
			new Title( name: "Under the Dome", type: "Scripted", language:  "English" ){
				ID = 1, 
				Genres = new string[]{ "Drama", "Science-Fiction", "Thriller" },
				Images = new Dictionary<string,string>(){ 
					{ "medium", "https://static.tvmaze.com/uploads/images/medium_portrait/0/6.jpg" },
					{ "original", "https://static.tvmaze.com/uploads/images/original_untouched/0/6.jpg" }
				}
			},

			new Title( name: "Person of Interest", type: "Scripted", language:  "English" ){
				ID = 2, 
				Genres = new string[]{ "Action", "Science-Fiction", "Crime" },
				Images = new Dictionary<string,string>(){ 
					{ "medium", "https://static.tvmaze.com/uploads/images/medium_portrait/0/6.jpg" },
					{ "original", "https://static.tvmaze.com/uploads/images/original_untouched/0/6.jpg" }
				}
			},

			new Title( name: "Bitten", type: "Scripted", language:  "English" ){
				ID = 3, 
				Genres = new string[]{ "Drama", "Horror", "Romance" },
				Images = new Dictionary<string,string>(){ 
					{ "medium", "https://static.tvmaze.com/uploads/images/medium_portrait/0/6.jpg" },
					{ "original", "https://static.tvmaze.com/uploads/images/original_untouched/0/6.jpg" }
				}
			}
		);
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