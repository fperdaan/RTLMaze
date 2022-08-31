namespace RTLMaze.Core.Scraper;

public interface IStreamProcessor<T> where T : class
{
	public T? Process( Stream input );
	public T? Process( ISource input );
}