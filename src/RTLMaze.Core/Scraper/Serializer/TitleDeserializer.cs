using System.Text.Json;
using RTLMaze.Models;

namespace RTLMaze.Core.Scraper.Serializer;

public partial class TitleDeserializer : OverloadJsonConverter<Title>
{
	// Fallback on default deserialization method
	public override Title? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		JsonDocument? document;

		if( !JsonDocument.TryParseValue( ref reader, out document ) )
			return null;

		Title? title = _ReadTitleBase( document, options );

		if( title == null )
			return null;

	 	ICollection<Cast>? cast = _ReadCast( document, options );

		if( cast != null )
		{	
			// -- set back reference
			foreach( var c in cast )
				c.Title = title;

			title.Cast = cast;
		}

		return title;
	}

	protected virtual Title? _ReadTitleBase( JsonDocument document, JsonSerializerOptions options )
	{
		return document.RootElement.Deserialize<Title>( _CopyOptionsAndRemove( options ) );
	}

	protected virtual ICollection<Cast>? _ReadCast( JsonDocument document, JsonSerializerOptions options )
	{
		JsonElement jsonEmbedded;

		if( !document.RootElement.TryGetProperty( "_embedded", out jsonEmbedded ) )
			return null;

		JsonElement jsonCast;

		if( !jsonEmbedded.TryGetProperty( "cast", out jsonCast ) )
			return null;

		return jsonCast.Deserialize<List<Cast>>( options );
	}

}