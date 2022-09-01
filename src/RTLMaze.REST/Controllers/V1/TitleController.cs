using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTLMaze.DAL;
using RTLMaze.Models;
using RTLMaze.REST.ViewModel;
using RTLMaze.REST.ViewModel.V1;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]"), Route("api/latest/[controller]")]
public partial class TitleController : AtomicController<Title, TitleViewModel>
{
	public TitleController( IRepository<Title> repo  ) : base( repo ) {}

	protected override TitleViewModel _CastObject( Title obj )
	{
		return new TitleViewModel( obj );
	}

	protected override IQueryable<TitleViewModel> _CastQuery( IQueryable<Title> query )
	{
		return query.Select( o => new TitleViewModel( o ) );
	}
}