namespace RTLMaze.Models;

public class StorableEntity : IStorableEntity
{
	public int ID { get; set; }
	public long Updated { get; set; }
}