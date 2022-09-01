using RTLMaze.Models;

namespace RTLMaze.REST.ViewModel.V1;

public partial class TitleViewModel
{
	public int ID { get; set; }
	public string Name { get; set; }
	public ICollection<PersonViewModel> Cast { get; set; }

	public TitleViewModel( Title title )
	{
		ID = title.ID;
		Name = title.Name;

		Cast = title.Cast
				.OrderBy( p => p.Person.Birthday )
				.Select( p => new PersonViewModel( p.Person ) )
				.ToList();	
	}
}