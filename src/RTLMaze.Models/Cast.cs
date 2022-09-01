namespace RTLMaze.Models;

public partial class Cast : ICast
{
	public IPerson Person { get; set; }
	public ITitle Title { get; }
	public string Name { get; set; }

	public Cast( IPerson person, ITitle title, string name )
	{
		Person = person;
		Title = title;
		Name = name;
	}
}