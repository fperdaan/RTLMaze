using System.Net;
using System.Text.Json;
using Polly;
using Polly.RateLimit;

namespace RTLMaze.Core;

public class ScraperOptions
{
	public string UpdateUrl { get; set; } = "";
	public Func<int, string> DetailUrl { get; set; } = ( int id ) => "";

	public IAsyncPolicy<HttpResponseMessage>? HttpRequestPolicy { get; set; }
	public RateLimitPolicy HttpRateLimiterPolicy { get; set; } = Policy.RateLimit( 20, TimeSpan.FromSeconds( 10 ), 5 );

	public ICollection<HttpStatusCode>? HttpStatusCodesWorthRetrying { get; set; }

	public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions();
}