using System.Net;
using Microsoft.Extensions.Options;
using Polly;

namespace RTLMaze.Core.Scraper;

public partial class HttpSource : IHttpSource
{
	private string? _sourceUrl;
	private IAsyncPolicy<HttpResponseMessage>? _requestPolicy;

	public IAsyncPolicy<HttpResponseMessage> RequestPolicy 
	{
		get {
			if( _requestPolicy == null )
				_requestPolicy = _GetDefaultRequestPolicy();
			
			return _requestPolicy;
		}
	}


	public HttpSource( string source = "" )
	{
		_sourceUrl = source;
	}

	public HttpSource( IOptions<ScraperOptions> options ) : this( options.Value ) { }
	public HttpSource( ScraperOptions options ) : this( "" )
	{
		//_requestPolicy = options.HttpRequestPolicy;
	}

	protected virtual IAsyncPolicy<HttpResponseMessage> _GetDefaultRequestPolicy()
	{
		var codes = new HttpStatusCode[]{
			HttpStatusCode.RequestTimeout, // 408
			HttpStatusCode.InternalServerError, // 500
			HttpStatusCode.BadGateway, // 502
			HttpStatusCode.ServiceUnavailable, // 503
			HttpStatusCode.GatewayTimeout // 504
		};

		var policy = Policy
				.Handle<HttpRequestException>()
				.OrResult<HttpResponseMessage>( r => codes.Contains( r.StatusCode ) )
				.RetryAsync( 3 );

		return policy;
	}

	# region Fluid interface

	public virtual IHttpSource FromUrl( string sourceUrl )
	{
		_sourceUrl = sourceUrl;

		return this;
	}

	public virtual IHttpSource SetRequestPolicy( IAsyncPolicy<HttpResponseMessage> policy )
	{
		_requestPolicy = policy;

		return this;
	}

	# endregion

	# region Source logic
	public virtual Stream GetSource()
	{
		HttpClient client = new HttpClient();

		var result = RequestPolicy.ExecuteAsync( () => client.GetAsync( _sourceUrl ) ).Result;

		return result.Content.ReadAsStream();
	}
	
	# endregion
}