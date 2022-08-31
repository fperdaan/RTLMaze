namespace RTLMaze.REST.Models;

static public class UriBuilderPagination 
{
	static public UriBuilder SetSkip( this UriBuilder builder, int value )
	{
		value = Math.Max( value, 0 );

		return value > 0 ? builder.ReplaceQuery( "$skip", value ) : builder.RemoveQuery( "$skip" );
	}	

	static public UriBuilder SetTop( this UriBuilder builder, int value ) => builder.ReplaceQuery( "$top", Math.Abs( value ) );
}