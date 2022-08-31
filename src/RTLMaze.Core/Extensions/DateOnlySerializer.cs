using Newtonsoft.Json;

namespace RTLMaze.Core.Extensions;

public class DateOnlySerializer : JsonConverter<DateOnly>
{
    public const string DATE_FORMAT = "yyyy-MM-dd";

	public override DateOnly ReadJson( JsonReader reader, Type typeToConvert, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer ) => 
		DateOnly.ParseExact( (string)reader.Value!, DATE_FORMAT );
	
	public override void WriteJson( JsonWriter writer, DateOnly value, JsonSerializer serializer ) => 
		writer.WriteValue( value.ToString( DATE_FORMAT ) );
}

public class DateOnlySerializerNullable : JsonConverter<DateOnly?>
{
    public const string DATE_FORMAT = "yyyy-MM-dd";

	public override DateOnly? ReadJson( JsonReader reader, Type typeToConvert, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer )
	{
		DateOnly result;
		DateOnly.TryParseExact( (string)reader.Value!, DATE_FORMAT, out result );

		return result;		
	}

	public override void WriteJson( JsonWriter writer, DateOnly? value, JsonSerializer serializer ) => 
		writer.WriteValue( value?.ToString( DATE_FORMAT ) );
}