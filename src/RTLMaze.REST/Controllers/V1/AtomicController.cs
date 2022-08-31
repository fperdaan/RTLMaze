using Microsoft.AspNetCore.Mvc;
using System.Net;

using RTLMaze.DAL;
using RTLMaze.Models;
using RTLMaze.REST.ViewModel;

namespace RTLMaze.REST.Controllers.V1;

abstract public class AtomicController<T> : ControllerBase where T : IStorableEntity
{
	protected IRepository<T> _repo;

	public AtomicController( IRepository<T> repo )
	{
		this._repo = repo;
	}

	 [HttpGet, Route("$count")]
    public virtual int Count()
    {	
		return this._repo.Query().Count();
    }

    [HttpGet, Route("{id}")]
    public virtual async Task<Response<T>> Get( int id )
    {	
		T? result = await this._repo.GetById( id );

		if( result != null )
			return new Response<T>( result );

		else 
			return new ResponseError<T>( "id", "Unable to find the object with the specified id", HttpStatusCode.NotFound );
    }

    [HttpGet, Route("")]
    public virtual ResponsePaged<T> List( [FromQuery] Pagination pagination )
    {
		return ResponsePaged<T>.ToPagedResponse(
			source: this._repo.Query().OrderBy( p => p.ID ),
			request: Request,
			top: pagination.Top, 
			skip: pagination.Skip
		);
    }
}