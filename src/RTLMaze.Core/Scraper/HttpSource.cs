using System.Net;

namespace RTLMaze.Core.Scraper;

public partial class HttpSource : IHttpSource
{
	private int _requestMaxAttempts = 5;
	private int _requestsTimeout = 1000;
	private string? _source;
	protected ICollection<HttpStatusCode> _retryStatusWhitelist = new List<HttpStatusCode> { HttpStatusCode.TooManyRequests };

	public HttpSource( string source = "" )
	{
		_source = source;
	}

	# region Fluid interface
	public virtual IHttpSource RetryOnStatuscode( HttpStatusCode code ) 
	{
		_retryStatusWhitelist.Add( code );

		return this;
	}

	public virtual IHttpSource FromUrl( string source )
	{
		_source = source;

		return this;
	}

	public virtual IHttpSource SetMaxRequestAttempts( int amount )
	{
		_requestMaxAttempts = amount;

		return this;
	}

	public virtual IHttpSource SleepSecondsBetweenAttempts( int sleep )
	{
		_requestsTimeout = sleep;

		return this;
	}

	# endregion

	# region Source logic
	public virtual Stream GetSource()
	{
		if( _source == null )
			throw new ArgumentException( "source", "No source was specified" );

		return _GetSource( 1 ).Result;		
	}

	protected virtual async Task<Stream> _GetSource( int attempt )
	{
		HttpClient client = new HttpClient();
		var response = await client.GetAsync( _source );

		// -- Check for valid response and reboot if failed
		if( response.IsSuccessStatusCode )
			return response.Content.ReadAsStream();

		if( _retryStatusWhitelist.Contains( response.StatusCode )  && attempt < _requestMaxAttempts )
		{	
			// Delay task and try to fetch it again 
			await Task.Delay( _requestsTimeout );

			return await _GetSource( attempt + 1 );
		}
		
		throw new SourceUnavailibleException("Unable to connect with the specified source");
	}
	
	# endregion
}