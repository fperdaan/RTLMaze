using Microsoft.EntityFrameworkCore;
using RTLMaze.Models;
using System.Linq.Expressions;

namespace RTLMaze.DAL;

public partial class AtomicRepository<T> : IRepository<T> where T : StorableEntity 
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

	public virtual async Task Delete( int id )
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
		return _context.Set<T>();
	}

	public virtual async Task<IEnumerable<T>> GetAll()
	{
        return await _context.Set<T>().ToListAsync();
	}

	public virtual async Task<T?> GetById( int id )
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
}