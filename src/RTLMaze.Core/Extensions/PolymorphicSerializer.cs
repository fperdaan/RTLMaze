using System.Text.Json;
using RTLMaze.Core;

namespace School.REST.Configuration;

public class PolymorphicSerializer<T> : OverloadJsonConverter<T> where T : class
{
	// Overwrite the write method to convert to the polymorphic type ( if availible )
	public override void Write( Utf8JsonWriter writer, T value, JsonSerializerOptions options ) => 
		writer.WriteRawValue( JsonSerializer.Serialize( (object) value ) );
}
