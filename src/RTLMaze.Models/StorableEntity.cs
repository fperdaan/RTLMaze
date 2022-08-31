namespace RTLMaze.Models;

public partial class StorableEntity : IStorableEntity
{
	public int ID { get; set; }
	public long Updated { get; set; }
}