using System.Net;
using Polly;

namespace RTLMaze.Core.Scraper;

public interface IHttpSource : ISource<Stream>
{
	public IHttpSource FromUrl( string sourceUrl );

	public IHttpSource SetRequestPolicy( IAsyncPolicy<HttpResponseMessage> policy );
}