using RTLMaze.Models;

namespace RTLMaze.REST.ViewModel.V1;

public partial class PersonViewModel
{
	public int ID { get; set; }
	public string Name { get; set; }
	public DateOnly? Birthday { get; set; }

	public PersonViewModel( Person person )
	{
		ID = person.ID;
		Name = person.Name;
		Birthday = person.Birthday;
	}
}