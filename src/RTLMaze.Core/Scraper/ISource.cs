namespace RTLMaze.Core.Scraper;

public interface ISource<TOutput>
{
	public TOutput GetSource();
}