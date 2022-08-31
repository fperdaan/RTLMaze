namespace RTLMaze.Models;

public class Title : StorableEntity, ITitle
{
	public string Name { get; set; }
	public string Type { get; set; }
	public string Language { get; set; }
	public ICollection<string> Genres { get; set; } = new List<string>();
	public IDictionary<string, string> Images { get; set; } = new Dictionary<string, string>();

	public Title( string name, string type, string language )
	{
		Name = name;
		Type = type;
		Language = language;
	}
}