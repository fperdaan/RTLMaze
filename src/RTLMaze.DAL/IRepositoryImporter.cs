namespace RTLMaze.DAL;

public interface IRepositoryImporter<T>
{
	public Task Import( T item );
	public Task Import( IEnumerable<T> items );
	public Task Process();
}