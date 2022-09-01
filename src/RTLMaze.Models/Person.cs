namespace RTLMaze.Models;

public partial class Person : StorableEntity
{
	public string Name { get; private set; }
	public DateOnly? Birthday { get; set; }

	public Person( string name )
	{
		Name = name;
	}
}