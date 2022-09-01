using Microsoft.Extensions.DependencyInjection;
using RTLMaze.Core.Scraper;
using System.Text.Json;

namespace RTLMaze.Core;

static public class Configure
{
	public static void ConfigureServices( IServiceCollection services  )
	{
		// -- Add global serializer configuration
		services.Configure<ScraperOptions>( options => {
			options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
			
			options.JsonSerializerOptions.Converters.Add( new DateOnlySerializer() );
		});

		// -- Register our DI
		services.AddTransient( typeof( IJsonStreamProcessor<> ), typeof( JsonStreamProcessor<> ) );
	}
}