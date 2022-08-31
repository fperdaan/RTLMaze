using System.Text.Json;
using System.Text.Json.Serialization;
using RTLMaze.Models;

namespace RTLMaze.Core;

public class CountrySerializer : JsonConverter<ICountry>
{
	public override ICountry? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options ) =>
		JsonSerializer.Deserialize<Country>( ref reader, options );

	public override void Write( Utf8JsonWriter writer, ICountry value, JsonSerializerOptions options ) => 
		writer.WriteRawValue( JsonSerializer.Serialize( (object) value ) );

}