namespace RTLMaze.Models;

public class Cast : ICast
{
	public IPerson Person { get; set; }
	public ITitle Title { get; set; }
	public string Name { get; set; }
	public Dictionary<string, string> Images { get; set; } = new Dictionary<string, string>();
}