namespace RTLMaze.Models;

public class Title : StorableEntity, ITitle
{
	public string Name { get; set; }
	public string Language { get; set; }
	public ICollection<string> Genres { get; set; } = new List<string>();
}