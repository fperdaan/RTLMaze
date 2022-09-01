using System.Text.Json.Serialization;

namespace RTLMaze.Models;

public partial class Title : StorableEntity, ITitle
{
	public string Name { get; set; }

	public IEnumerable<ICast> Cast { get; set; } = new List<ICast>();

	public Title( string name )
	{
		Name = name;
	}
}