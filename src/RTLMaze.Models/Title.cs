using System.Text.Json.Serialization;

namespace RTLMaze.Models;

public partial class Title : StorableEntity, ITitle
{
	public string Name { get; set; }

	public Title( string name )
	{
		Name = name;
	}
}