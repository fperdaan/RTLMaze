namespace RTLMaze.Models;

public partial class Title : StorableEntity
{
	/// <summary>
	/// Unix timestamp indicating when the item was last updated
	/// </summary>
	public long Updated { get; set; }
	public string Name { get; set; }
	public virtual ICollection<Cast> Cast { get; set; }

	public Title( string name )
	{
		Name = name;
		Cast = new List<Cast>();
	}
}