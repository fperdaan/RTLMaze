namespace RTLMaze.Models;

abstract public partial class StorableEntity : IStorableEntity
{
	public int ID { get; set; }
	public long Updated { get; set; }
}