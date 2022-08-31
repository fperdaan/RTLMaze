namespace RTLMaze.Models;

public interface IStorableEntity
{
	public int ID { get; set; }

	/// <summary>
	/// Unix timestamp indicating when the item was last updated
	/// </summary>
	public long Updated { get; set; }
}