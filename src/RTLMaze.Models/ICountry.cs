namespace RTLMaze.Models;

public interface ICountry
{
	public string Code { get; set; }
	public string Name { get; set; }

	// Maybe this should probabily be a TimeZoneInfo
	public string Timezone { get; set; }
}