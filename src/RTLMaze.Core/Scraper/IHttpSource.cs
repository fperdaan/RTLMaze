using System.Net;

namespace RTLMaze.Core.Scraper;

public interface IHttpSource : ISource
{
	public IHttpSource RetryOnStatuscode( HttpStatusCode code );

	public IHttpSource FromUrl( string source );

	public IHttpSource SetMaxRequestAttempts( int amount );

	public IHttpSource SleepSecondsBetweenAttempts( int sleep );
}