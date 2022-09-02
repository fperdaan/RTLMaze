using Microsoft.Extensions.Options;

namespace RTLMaze.Core.Scraper;

public class ChangedTitleSource : ISource<ICollection<int>>
{
	public long? SinceDate { get; set; }
	public ScraperOptions _options { get; set; }

	public ChangedTitleSource( IOptions<ScraperOptions> options ) : this( options.Value ){ }
	public ChangedTitleSource( ScraperOptions options )
	{
		_options = options;
	}

	public ICollection<int> GetSource()
	{
		var result = new JsonStreamProcessor<Dictionary<string, int>>( _options )
						.Process( new HttpSource( _options ).FromUrl( _options.UpdateUrl ) );
						

		var query = result.AsQueryable();

		if( SinceDate != null )
			query = query.Where( kv => kv.Value >= SinceDate );

		return query
				.Select( kv => Int32.Parse( kv.Key ) )
				.ToList();
	}
}