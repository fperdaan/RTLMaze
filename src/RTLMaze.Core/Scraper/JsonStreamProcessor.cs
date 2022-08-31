using System.Text.Json;
using System.Text.Json.Serialization;

namespace RTLMaze.Core.Scraper;

public partial class JsonStreamProcessor<T> : IStreamProcessor<T> where T : class
{
	private JsonSerializerOptions JsonOptions { get; set; } = new JsonSerializerOptions();
	
	# region Fluent interface

	public virtual JsonStreamProcessor<T> SetJsonOptions( JsonSerializerOptions options )
	{
		JsonOptions = options;

		return this;
	}

	public virtual JsonStreamProcessor<T> AddConverter( JsonConverter<T> converter )
	{
		JsonOptions.Converters.Add( converter );

		return this;
	}

	# endregion

	public virtual T? Process( Stream input )
	{
		T? result;

		try 
		{
			using ( StreamReader reader = new StreamReader( input ) )
			{
				string json = reader.ReadToEnd();
				result = JsonSerializer.Deserialize<T>( json, JsonOptions );
			}
		}
		catch( Exception e )
		{
			throw new JsonFormatException("The supplied stream does not match the expected json format", e);
		}

		return result;
	}

	public T? Process( ISource input ) => Process( input.GetSource() );
}