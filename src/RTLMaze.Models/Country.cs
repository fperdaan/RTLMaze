namespace RTLMaze.Models;

public partial class Country : ICountry
{
	public string Code { get; set; }
	public string Name { get; set; }
	public string Timezone { get; set; }
}