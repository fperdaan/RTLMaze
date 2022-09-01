using Microsoft.AspNetCore.Mvc;
using System.Net;

using RTLMaze.DAL;
using RTLMaze.Models;
using RTLMaze.REST.ViewModel.V1;

namespace RTLMaze.REST.Controllers.V1;

abstract public partial class AtomicController<T, TViewModel> : ControllerBase where T : IStorableEntity
{
	protected IRepository<T> _repo;

	public AtomicController( IRepository<T> repo )
	{
		this._repo = repo;
	}

	abstract protected TViewModel _CastObject( T obj );
	abstract protected IQueryable<TViewModel> _CastQuery( IQueryable<T> query );

	[HttpGet, Route("$count")]
    public virtual int Count()
    {	
		return this._repo.Query().Count();
    }

    [HttpGet, Route("{id}")]
    public virtual async Task<Response<TViewModel>> Get( int id )
    {	
		T? result = await this._repo.GetById( id );

		if( result != null )
			return new Response<TViewModel>( _CastObject( result ) );

		else 
			return new ResponseError<TViewModel>( "id", "Unable to find the object with the specified id", HttpStatusCode.NotFound );
    }

    [HttpGet, Route("")]
    public virtual ResponsePaged<TViewModel> List( [FromQuery] Pagination pagination )
    {
		return ResponsePaged<TViewModel>.ToPagedResponse(
			source: _CastQuery( this._repo.Query().OrderByDescending( e => e.ID ) ),
			request: Request,
			top: pagination.Top, 
			skip: pagination.Skip
		);
    }
}

abstract public partial class AtomicController<T> : AtomicController<T, T> where T : IStorableEntity
{
	protected AtomicController( IRepository<T> repo ) : base(repo ) { }

	protected override T _CastObject( T obj ) => obj;
	protected override IQueryable<T> _CastQuery( IQueryable<T> query ) => query;
}