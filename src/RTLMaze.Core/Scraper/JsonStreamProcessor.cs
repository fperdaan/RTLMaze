using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace RTLMaze.Core.Scraper;

public partial class JsonStreamProcessor<T> : IJsonStreamProcessor<T>
{
	public JsonSerializerOptions JsonOptions { get; private set; }
	
	public JsonStreamProcessor() 
	{ 
		JsonOptions = new JsonSerializerOptions();
	}
	
	/// <summary>
	/// DI Interface, used to inherit the options from the DI configuration
	/// </summary>
	public JsonStreamProcessor( ScraperOptions options ) 
	{
		JsonOptions = options.JsonSerializerOptions;
	} 

	# region Fluent interface

	public virtual IJsonStreamProcessor<T> SetJsonOptions( JsonSerializerOptions options )
	{
		JsonOptions = options;

		return this;
	}

	public virtual IJsonStreamProcessor<T> AddConverter( JsonConverter<T> converter )
	{
		JsonOptions.Converters.Add( converter );

		return this;
	}

	# endregion

	public virtual T Process( Stream input )
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


		if( result == null )
			throw new JsonFormatException("The serialization resolved in an empty result");

		return result;
	}

	public T Process( ISource input ) => Process( input.GetSource() );
}