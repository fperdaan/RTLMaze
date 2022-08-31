namespace RTLMaze.Models;

public interface ICast
{
	public IPerson Person { get; set; }
	public ITitle Title { get; set; }

	public string Name { get; set; }
}