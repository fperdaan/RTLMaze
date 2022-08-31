namespace RTLMaze.Models;

public class Person : StorableEntity, IPerson
{
	public string Name { get; set; }
	public DateOnly BirthDay { get; set; }

	public Person( string name )
	{
		Name = name;
	}
}