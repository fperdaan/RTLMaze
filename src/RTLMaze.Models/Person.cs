namespace RTLMaze.Models;

public class Person : StorableEntity, IPerson
{
	public string Name { get; }
	public DateOnly? Birthday { get; set; }

	public Person( string name )
	{
		Name = name;
	}
}