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
	public Job? GetLastRunJob();
	public Job? GetLastSuccesfulRunJob();

	// Scraper / processing methods 
	public ICollection<int> FetchChangedTitles();
	public IEnumerable<Title> FetchTitleDetails( ICollection<int> ids );
	
	// Internal job interface
	public FluentJob Start();
	public FluentJob Finish();
}