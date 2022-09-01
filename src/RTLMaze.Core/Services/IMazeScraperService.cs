using RTLMaze.Core.Models;
using RTLMaze.Models;

namespace RTLMaze.Core.Services;

public interface IMazeScraperService
{
	public FluentJob Job { get; }

	// Configuration
	public IMazeScraperService Since( DateTime? date );

	// Scraper status info
	public bool IsAScraperRunning();
	public DateTime? GetLastRunTime();
	public Job? GetLastRunJob();

	// Internal job interface
	public FluentJob Start();
	public FluentJob Finish();
}