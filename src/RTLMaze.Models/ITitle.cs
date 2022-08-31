namespace RTLMaze.Models;

public interface ITitle : IStorableEntity
{
	public string Name { get; set; }
	public string Language { get; set; }

	// This should probabily be an enum, decide later
	public ICollection<string> Genres { get; set; }
}