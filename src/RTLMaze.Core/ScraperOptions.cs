using System.Text.Json;

namespace RTLMaze.Core;

public class ScraperOptions
{
	public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions();
}