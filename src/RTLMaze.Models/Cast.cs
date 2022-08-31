namespace RTLMaze.Models;

public partial class Cast : ICast
{
	public IPerson Person { get; set; }
	public ITitle Title { get; set; }
	public string Name { get; set; }
	public IDictionary<string, string> Image { get; set; } = new Dictionary<string, string>();

	public Cast( IPerson person, ITitle title, string name )
	{
		Person = person;
		Title = title;
		Name = name;
	}
}