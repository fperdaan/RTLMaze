using System.Text.Json;
using System.Text.Json.Serialization;
using RTLMaze.Models;

namespace RTLMaze.Core.Scraper.Serializer;

public partial class TitleDeserializer : OverloadJsonConverter<ITitle>
{
	// Fallback on default deserialization method
	public override ITitle? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		JsonDocument? document;

		if( !JsonDocument.TryParseValue( ref reader, out document ) )
			return null;

		ITitle? title = _ReadTitleBase( document, options );

		if( title == null )
			return null;

	 	IEnumerable<ICast>? cast = _ReadCast( document, options );

		if( cast != null )
			title.Cast = cast;

		return title;
	}

	protected virtual ITitle? _ReadTitleBase( JsonDocument document, JsonSerializerOptions options )
	{
		return document.RootElement.Deserialize<ITitle>( _CopyOptionsAndRemove( options ) );
	}

	protected virtual IEnumerable<ICast>? _ReadCast( JsonDocument document, JsonSerializerOptions options )
	{
		JsonElement jsonEmbedded;

		if( !document.RootElement.TryGetProperty( "_embedded", out jsonEmbedded ) )
			return null;

		JsonElement jsonCast;

		if( !jsonEmbedded.TryGetProperty( "cast", out jsonCast ) )
			return null;

		return jsonCast.Deserialize<List<ICast>>( options );
	}

}