namespace RTLMaze.DAL;

public interface IRepositoryImporter<T>
{
	public void Import( T item );
	public void Import( IEnumerable<T> items );
	public void Process();
}