using System.Net;
using System.Text.Json;

namespace RTLMaze.Core;

public class ScraperOptions
{
	public string UpdateUrl { get; set; } = "";
	public Func<int, string> DetailUrl { get; set; } = ( int id ) => "";

	public HttpSourceOptions HttpSourceOptions { get; set; } = new HttpSourceOptions();

	public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions();
}

public class HttpSourceOptions 
{
	public int RequestMaxAttempts { get; set; } = 5;
	public int RequestTimeout { get; set; } = 1000;
	public ICollection<HttpStatusCode> RetryOnStatusCode = new List<HttpStatusCode> { HttpStatusCode.TooManyRequests };
}