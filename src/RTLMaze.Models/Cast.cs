namespace RTLMaze.Models;

public class Cast : ICast
{
	public IPerson Person { get; set; }
	public ITitle Title { get; set; }
	public string Name { get; set; }
	public IDictionary<string, string> Images { get; set; } = new Dictionary<string, string>();

	public Cast( IPerson person, ITitle title, string name )
	{
		Person = person;
		Title = title;
		Name = name;
	}
}