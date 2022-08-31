namespace RTLMaze.Models;

public interface ITitle : IStorableEntity, IHaveImages
{
	public string Name { get; set; }
	
	// This should probabily be an enum, decide later
	public string Language { get; set; }

	// This should probabily be an enum, decide later
	public string Type { get; set; }

	// This should probabily be an enum, decide later
	public ICollection<string> Genres { get; set; }
}