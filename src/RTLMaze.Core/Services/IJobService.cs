using RTLMaze.Core.Models;
using RTLMaze.Models;

namespace RTLMaze.Core.Services;

public interface IJobService
{
	public Job GetLatestForType( string type );
	public Job? GetLastSuccessfulJob( string type );
	public DateTime? GetTimeWhenLastSuccessful( string type );
	public Task Save( Job job );
	public FluentJob NewJob( string type );
	public FluentJob ModifyJob( Job job );
}