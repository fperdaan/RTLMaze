using Microsoft.AspNetCore.Mvc;
using RTLMaze.DAL;
using RTLMaze.Models;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/management/job"), Route("api/latest/management/job")]
public partial class ManagementJobController : AtomicController<Job>
{
	public ManagementJobController( IRepository<Job> repo  ) : base( repo ) {}
}