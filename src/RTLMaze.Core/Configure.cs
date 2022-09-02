using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Polly;
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

			// ( todo: read from app settings )
			options.UpdateUrl = "https://api.tvmaze.com/updates/shows";
			options.DetailUrl = ( int id ) => $"https://api.tvmaze.com/shows/{id}?embed=cast";

			options.HttpStatusCodesWorthRetrying = new HttpStatusCode[]{
				HttpStatusCode.RequestTimeout, // 408
				HttpStatusCode.InternalServerError, // 500
				HttpStatusCode.BadGateway, // 502
				HttpStatusCode.ServiceUnavailable, // 503
				HttpStatusCode.GatewayTimeout // 504
			};

			// Set default policy ( todo: read from app settings )
			options.HttpRequestPolicy = Policy
				.HandleResult<HttpResponseMessage>( r => options.HttpStatusCodesWorthRetrying.Contains( r.StatusCode ) )
				.RetryAsync( 3 );

			options.HttpRateLimiterPolicy = Policy
				.RateLimit( 20, TimeSpan.FromSeconds( 10 ), 5 );

			// JsonSerializer
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