namespace RTLMaze.Models;

public interface IPerson : IHaveImages, IStorableEntity
{
	public string Name { get; set; }
	public DateOnly BirthDay { get; set; }
	public DateOnly DeathDay { get; set; }
	public Gender Gender { get; set; }
	public ICountry Country { get; set; }
}