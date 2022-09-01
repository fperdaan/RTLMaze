using System.Text.Json;
using System.Text.Json.Serialization;
using RTLMaze.Models;

namespace RTLMaze.Core;

public class OverloadJsonConverter<T> : JsonConverter<T>
{
	// Fallback on default deserialization method
	public override T? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options ) => 
		JsonSerializer.Deserialize<T>( ref reader, _CopyOptionsAndRemove( options ) );

	// Fallback on default serialization method
	public override void Write( Utf8JsonWriter writer, T value, JsonSerializerOptions options ) => 
		JsonSerializer.Serialize<T>( writer, value, _CopyOptionsAndRemove( options ) );

	// Copy the options set and filter out self to prevent looping
	protected virtual JsonSerializerOptions _CopyOptionsAndRemove( JsonSerializerOptions options )
	{
        var copy = new JsonSerializerOptions( options );
		
        for ( var i = copy.Converters.Count - 1; i >= 0; i--)
		{
            if ( copy.Converters[ i ] == this )
			{
                copy.Converters.RemoveAt( i );
				break;
			}
		}

        return copy;
	}
}