namespace RTLMaze.Models;

public interface IPerson : IStorableEntity
{
	public string Name { get; set; }
	public DateOnly BirthDay { get; set; }
}