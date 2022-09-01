namespace RTLMaze.DAL;

public interface IRepository<T>
{
	public Task Add( T item );
	public Task AddAll( IEnumerable<T> items );
	public Task Delete( int id );	
	public Task DeleteAll( IEnumerable<T> items );
	public Task<IEnumerable<T>> GetAll();
	public Task<T?> GetById( int id );
	public IQueryable<T> Query();
	public Task Save( T item );
	public Task SaveAll( IEnumerable<T> items );
}