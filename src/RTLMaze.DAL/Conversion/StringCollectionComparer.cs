using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RTLMaze.DAL.Conversion;

public class StringCollectionComparer : ValueComparer<ICollection<string>>
{
	public StringCollectionComparer() : base(
		(c1, c2) => c1 != null && c2 != null ? c1.SequenceEqual( c2 ) : false,
		c => c != null ? c.GetHashCode() : 0, 
		c => c.ToList()
	)
	{

	}
	
}