using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RTLMaze.Models;

public partial class Cast
{
	[JsonIgnore]
	public int ID { get; set; }
	
	[Required]
	public virtual Person Person { get; set; } = null!;

	[Required, JsonIgnore]
	public virtual Title Title { get; set; } = null!;

	public string Name { get; set; } = "";
}