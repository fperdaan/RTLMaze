namespace RTLMaze.Models;

public class Person : StorableEntity, IPerson
{
	public string Name { get; set; }
	public DateOnly BirthDay { get; set; }
	public DateOnly DeathDay { get; set; }
	public Gender Gender { get; set; }
	public ICountry Country { get; set; }
	public IDictionary<string, string> Images { get; set; } = new Dictionary<string, string>();

	public Person( string name, ICountry country )
	{
		Name = name;
		Country = country;
	}
}