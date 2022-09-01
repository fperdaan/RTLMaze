using Microsoft.Extensions.DependencyInjection;
using RTLMaze.Core.Scraper;
using RTLMaze.Core.Scraper.Serializer;
using RTLMaze.Core.Services;
using RTLMaze.Models;

namespace RTLMaze.Core;

static public class Configure
{
	public static void ConfigureServices( IServiceCollection services  )
	{
		// -- Add global serializer configuration
		services.Configure<ScraperOptions>( options => {
			options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
			
			options.JsonSerializerOptions.Converters.Add( new DateOnlySerializer() );

			options.JsonSerializerOptions.Converters.Add( new DateOnlyNullableSerializer() );

			options.JsonSerializerOptions.Converters.Add( new CastDeserializer() );
			options.JsonSerializerOptions.Converters.Add( new TitleDeserializer() );
		});

		// -- Register our scraper configuration
		services.AddTransient( typeof( IJsonStreamProcessor<> ), typeof( JsonStreamProcessor<> ) );

		// -- Register other services
		services.AddScoped<IJobService, JobService>();
		services.AddScoped<IMazeScraperService, MazeScraperService>();
	}
}