using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace RTLMaze.DAL.Conversion;

public class SerializerConversion<T, N> : ValueConverter<T, string> where N : T, new()
{
	public SerializerConversion() : base(
		from => JsonSerializer.Serialize( from, (JsonSerializerOptions)null! ),
		to => JsonSerializer.Deserialize<N>( to, (JsonSerializerOptions)null! ) ?? new N()
	)
	{
	}
}
