using System.Text.Json;
using RTLMaze.Models;

namespace RTLMaze.Core.Scraper.Serializer;

public partial class CastDeserializer : OverloadJsonConverter<ICast>
{
	// Fallback on default deserialization method
	public override ICast? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		JsonDocument? document;

		if( !JsonDocument.TryParseValue( ref reader, out document ) )
			return null;

		IPerson? person = _ReadPerson( document, options );

		if( person == null )
			return null;

		ICast? cast = _ReadCastBase( document, options );
		
		if( cast == null )
			return null;

		cast.Person = person;

		return cast;
	}

	protected virtual IPerson? _ReadPerson( JsonDocument document, JsonSerializerOptions options )
	{
		JsonElement jsonElement;

		if( !document.RootElement.TryGetProperty( "person", out jsonElement ) )
			return null;

		return jsonElement.Deserialize<IPerson>( options );
	}

	protected virtual ICast? _ReadCastBase( JsonDocument document, JsonSerializerOptions options )
	{
		JsonElement jsonElement;

		if( !document.RootElement.TryGetProperty( "character", out jsonElement ) )
			return null;

		return jsonElement.Deserialize<ICast>( _CopyOptionsAndRemove( options ) );
	}

}