namespace RTLMaze.REST.Models;
using Microsoft.AspNetCore.Http.Extensions;

static public class UriBuilderPagination 
{
	static public UriBuilder SetSkip( this UriBuilder builder, int value )
	{
		value = Math.Max( value, 0 );

		return value > 0 ? builder.ReplaceQuery( "$skip", value ) : builder.RemoveQuery( "$skip" );
	}	

	static public UriBuilder SetTop( this UriBuilder builder, int value ) => builder.ReplaceQuery( "$top", Math.Abs( value ) );
}

static public class UriBuilderRequestExtension 
{
	static public UriBuilder GetUriBuilder( this HttpRequest request )
	{
		return new UriBuilder( request.GetDisplayUrl() );
	}
}
