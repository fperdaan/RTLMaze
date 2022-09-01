using System.Net;
using Microsoft.AspNetCore.Mvc;
using RTLMaze.DAL;
using RTLMaze.Models;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/management/job"), Route("api/latest/management/job")]
public partial class ManagementJobController : AtomicController<Job>
{
	public ManagementJobController( IRepository<Job> repo  ) : base( repo ) {}

	[HttpGet, Route("{id}/cancel")]
	public async virtual Task<Response<Job>> Cancel( int id )
	{
		Job? job = await this._repo.GetById( id );

		if( job == null )
			return new ResponseError<Job>( "id", "Unable to find the object with the specified id", HttpStatusCode.NotFound );

		if( job.Status != JobStatus.Running )
			return new ResponseError<Job>( "status", "Only running jobs can be cancelled", HttpStatusCode.BadRequest );

		job.Status = JobStatus.Aborted;
		job.End();

		await _repo.Save( job );

		return new Response<Job>( job );
	}
}