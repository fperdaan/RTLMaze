namespace RTLMaze.Models;

public partial class Title : StorableEntity
{
	public string Name { get; set; }
	
	public virtual ICollection<Cast> Cast { get; set; }

	public Title( string name )
	{
		Name = name;
		Cast = new List<Cast>();
	}
}