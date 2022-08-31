using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions;

namespace RTLMaze.REST.Models.Responses;

public class ResponsePaged<T> : Response<IAsyncEnumerable<T>>
{
	//public Pagination Pagination { get; }

	[JsonPropertyName("$odata.nextLink")]
	public string? NextLink { get; }

	[JsonPropertyName("$odata.prevLink")]
	public string? PrevLink { get; }

	private ResponsePaged( IAsyncEnumerable<T> items, HttpRequest request, int count, int top, int skip ) : base( items )
	{
		if( top + skip < count )
		{
			this.NextLink = request.GetUriBuilder().SetSkip( top + skip ).ToString();
		}

		if( skip > 0 )
		{
			this.PrevLink = request.GetUriBuilder().SetSkip( top - skip ).ToString();
		}
	}

	public static ResponsePaged<T> ToPagedResponse( IQueryable<T> source, HttpRequest request, int top, int skip )
    {
        var count = source.Count();
        var items = source.Skip( skip ).Take( top ).AsAsyncEnumerable();
		
        return new ResponsePaged<T>( items, request, count, top, skip );
    }
}