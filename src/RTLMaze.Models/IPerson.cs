namespace RTLMaze.Models;

public interface IPerson : IStorableEntity
{
	public string Name { get; }
	public DateOnly? Birthday { get; set; }
}