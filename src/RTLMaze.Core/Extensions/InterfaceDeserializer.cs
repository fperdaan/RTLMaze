using System.Text.Json;

namespace RTLMaze.Core;

public class InterfaceDeserializer<TInterface, TClass> : OverloadJsonConverter<TInterface> where TClass : class, TInterface
{
	public override TInterface? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options ) => 
		JsonSerializer.Deserialize<TClass>( ref reader, options );
}