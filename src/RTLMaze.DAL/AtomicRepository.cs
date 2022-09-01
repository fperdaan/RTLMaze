using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RTLMaze.DAL;

public partial class AtomicRepository<T, PKeyType> : IRepository<T, PKeyType> where T : class
{
	private RTLMazeStorageContext _context;

	public AtomicRepository( RTLMazeStorageContext context )
	{
		_context = context;
	}

	public virtual async Task Add( T item )
	{
		_context.Add( item );

		await _context.SaveChangesAsync();
	}

	public virtual async Task AddAll( IEnumerable<T> items )
	{
		_context.AddRange( items );

		await _context.SaveChangesAsync();
	}

	public virtual async Task Delete( PKeyType id )
	{
		T? obj = await GetById( id );

		if( obj != null )
		{
			_context.Remove( obj );

			await _context.SaveChangesAsync();
		}
	}

	public virtual async Task DeleteAll( IEnumerable<T> items )
	{
		_context.RemoveRange( items );

		await _context.SaveChangesAsync();
	}

	public IQueryable<T> Query()
	{
		return _context.Set<T>().AsNoTracking();
	}

	public virtual async Task<IEnumerable<T>> Get<T2>( Expression<Func<T, bool>> predicate )
	{
    	return await _context.Set<T>().AsNoTracking().Where( predicate ).ToListAsync();
	}

	public virtual async Task<IEnumerable<T>> GetAll()
	{
        return await _context.Set<T>().AsNoTracking().ToListAsync();
	}

	public virtual async Task<T?> GetById( PKeyType id )
	{
		return await _context.Set<T>().FindAsync( id );
	}

	public virtual async Task Save( T item )
	{
		_context.Update( item );

		await _context.SaveChangesAsync();
	}

	public virtual async Task SaveAll( IEnumerable<T> items )
	{
		_context.UpdateRange( items );

		await _context.SaveChangesAsync();
	}

	public virtual async Task SaveAllLazy( IEnumerable<T> items )
	{
		foreach( T item in items )
		{
			if( _context.Set<T>().Contains( item ) )	
				_context.Update( item );
			else 
				_context.Add( item );
		}

		await _context.SaveChangesAsync();
	}
}

// -- Quick interface
public class AtomicRepository<T> : AtomicRepository<T, int>, IRepository<T> where T : class
{
	public AtomicRepository( RTLMazeStorageContext context ) : base( context ) {}
} 