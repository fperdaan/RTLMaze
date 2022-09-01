using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RTLMaze.Models;

namespace RTLMaze.DAL;

public partial class TitleRepositoryImporter : IRepositoryImporter<Title>
{
	private RTLMazeStorageContext _context;

	public TitleRepositoryImporter( RTLMazeStorageContext context )
	{
		_context = context;
	}

	# region Helper methods regarding entity state

	protected virtual TEntity? _updateOrInsert<TEntity>( TEntity item ) where TEntity : class, IStorableEntity
	{
		TEntity? existingItem = _context.Set<TEntity>().Find( item.ID );

		if( existingItem != null )
		{
			_context.Entry( existingItem ).State = EntityState.Modified;
			_context.Entry( existingItem ).CurrentValues.SetValues( item );

			Console.WriteLine( $"Updated: {existingItem}" );
		}
		else 
		{
			_context.Entry( item ).State = EntityState.Added;

			Console.WriteLine( $"Added: {item}" );
		}

		return existingItem;
	}
	
	protected virtual void _markDeleted( IEnumerable<object> list )
	{
		foreach( object item in list )
			_context.Entry( item ).State = EntityState.Deleted;
	}

	# endregion

	public virtual void Import( Title title )
	{
		Title? exists = _updateOrInsert<Title>( title );

		Console.WriteLine( $"exists: {exists}" );

		// -- mark existing children as deleted; gets overwritten by update loop later (if still persist)
		if( exists != null )
			_markDeleted( exists.Cast );

		foreach( Cast cast in title.Cast )
		{
			_updateOrInsert<Person>( cast.Person );
			_updateOrInsert<Cast>( cast );
		}
	}

	public virtual void Import( IEnumerable<Title> items )
	{
		foreach( Title item in items )
			Import( item );
	}

	public virtual void Process()
	{
		Console.WriteLine("Trying to process data" );

		_context.SaveChanges();

		Console.WriteLine("Processed data" );
	}
}