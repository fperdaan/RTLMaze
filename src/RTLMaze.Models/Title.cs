using System.Text.Json.Serialization;

namespace RTLMaze.Models;

public partial class Title : StorableEntity, ITitle
{
	public string Name { get; set; }
	public string Type { get; set; }
	public string Language { get; set; }
	public ICollection<string> Genres { get; set; } = new List<string>();

	[JsonConverter(typeof(Dictionary<string, string>))]
	public IDictionary<string, string> Image { get; set; } = new Dictionary<string, string>();
	public string Status { get => Ended == null ? "Running" : "Ended"; }
	public DateOnly Premiered { get; set; }
	public DateOnly? Ended { get; set; }

	public string? Summary { get; set; }

	public Title( string name, string type, string language, DateOnly premiered )
	{
		Name = name;
		Type = type;
		Language = language;
		Premiered = premiered;
	}
}