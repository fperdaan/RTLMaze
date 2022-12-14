using System.Text.Json;
using System.Text.Json.Serialization;

namespace RTLMaze.Core;

public class DateOnlySerializer : JsonConverter<DateOnly>
{
    public const string DATE_FORMAT = "yyyy-MM-dd";

	public override DateOnly Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options ) => 
		DateOnly.ParseExact( reader.GetString()!, DATE_FORMAT );
	
	public override void Write( Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options ) =>
		writer.WriteStringValue( value.ToString( DATE_FORMAT ) );
}


public class DateOnlyNullableSerializer : JsonConverter<DateOnly?>
{
    public const string DATE_FORMAT = "yyyy-MM-dd";

	public override DateOnly? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{	
		string? data = reader.GetString();

		if( data != null )
			return DateOnly.ParseExact( data, DATE_FORMAT );
		else 
			return null;
	}
	
	public override void Write( Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options ) =>
		writer.WriteStringValue( value?.ToString( DATE_FORMAT ) );
}
