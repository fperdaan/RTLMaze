using System.Text.Json;
using System.Text.Json.Serialization;

namespace RTLMaze.Core.Scraper;

public interface IJsonStreamProcessor<T> : IStreamProcessor<T> where T : class
{
	public IJsonStreamProcessor<T> SetJsonOptions( JsonSerializerOptions options );	
	public IJsonStreamProcessor<T> AddConverter( JsonConverter<T> converter );
}