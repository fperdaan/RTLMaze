using System.Linq.Expressions;

namespace RTLMaze.DAL;

public interface IRepository<T, PKeyType>
{
	public Task Add( T item );
	public Task AddAll( IEnumerable<T> items );
	public Task Delete( PKeyType id );	
	public Task DeleteAll( IEnumerable<T> items );
	public Task<IEnumerable<T>> GetAll();
	public Task<T?> GetById( PKeyType id );
	public IQueryable<T> Query();
	public Task Save( T item );
	public Task SaveAll( IEnumerable<T> items );
	public Task SaveAllLazy( IEnumerable<T> items );
}

// Quick interface
public interface IRepository<T> : IRepository<T, int> 
{

}