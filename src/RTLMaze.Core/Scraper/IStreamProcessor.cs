namespace RTLMaze.Core.Scraper;

public interface IStreamProcessor<T>
{
	public T Process( Stream input );
	public T Process( ISource<Stream> input );
}