using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RTLMaze.DAL.Conversion;

public class StringCollectionConversion : ValueConverter<ICollection<string>, string>
{
	public StringCollectionConversion() : base(
		from => string.Join( ',', from ),
		to => to.Split(',', StringSplitOptions.None).ToList()
	)
	{
	}
}
