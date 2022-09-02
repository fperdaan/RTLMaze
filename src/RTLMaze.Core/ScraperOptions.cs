using System.Text.Json;

namespace RTLMaze.Core;

public class ScraperOptions
{
	public string UpdateUrl { get; set; } = "";
	public Func<int, string> DetailUrl { get; set; } = ( int id ) => "";

	public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions();
}