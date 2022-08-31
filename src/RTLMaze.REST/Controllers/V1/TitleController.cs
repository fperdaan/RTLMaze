using Microsoft.AspNetCore.Mvc;
using RTLMaze.DAL;
using RTLMaze.Models;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]"), Route("api/latest/[controller]")]
public class TitleController : AtomicController<Title>
{
	public TitleController( IRepository<Title> repo  ) : base( repo ) {}
}