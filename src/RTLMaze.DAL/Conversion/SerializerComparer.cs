using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RTLMaze.DAL.Conversion;

public class SerializerComparer<T> : ValueComparer<T>
{
	public SerializerComparer() : base (
		(c1, c2) => c1 != null && c1.Equals( c2 ),
		c => c != null ? c.GetHashCode() : 0
	)
	{
		
	}
}