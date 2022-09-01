using System.Text.Json;
using RTLMaze.Models;

namespace RTLMaze.Core.Scraper.Serializer;

public partial class CastDeserializer : OverloadJsonConverter<Cast>
{
	// Fallback on default deserialization method
	public override Cast? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		JsonDocument? document;

		if( !JsonDocument.TryParseValue( ref reader, out document ) )
			return null;

		Person? person = _ReadPerson( document, options );

		if( person == null )
			return null;

		Cast? cast = _ReadCastBase( document, options );
		
		if( cast == null )
			return null;

		cast.Person = person;

		return cast;
	}

	protected virtual Person? _ReadPerson( JsonDocument document, JsonSerializerOptions options )
	{
		JsonElement jsonElement;

		if( !document.RootElement.TryGetProperty( "person", out jsonElement ) )
			return null;

		return jsonElement.Deserialize<Person>( options );
	}

	protected virtual Cast? _ReadCastBase( JsonDocument document, JsonSerializerOptions options )
	{
		JsonElement jsonElement;

		if( !document.RootElement.TryGetProperty( "character", out jsonElement ) )
			return null;

		return jsonElement.Deserialize<Cast>( _CopyOptionsAndRemove( options ) );
	}

}