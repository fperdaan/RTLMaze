using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;

namespace RTLMaze.REST.Models;

public class UriBuilder : System.UriBuilder
{
	private NameValueCollection _query;

	public new string Query
	{
		get { return _query.ToString() ?? ""; }
		set { _query = HttpUtility.ParseQueryString( value ); }
	}
	
	public UriBuilder( string uri ) : base( uri ) 
	{
		this._query = HttpUtility.ParseQueryString( base.Query );
	}

	public UriBuilder AddQuery( string name, string value )
	{
		this._query.Add( name, value );
		base.Query = this.Query;

		return this;
	}
	public UriBuilder AddQuery( string name, int value ) => AddQuery( name, value.ToString() );
	public UriBuilder AddQuery( string name, double value ) => AddQuery( name, value.ToString() );
	public UriBuilder AddQuery( string name, float value ) => AddQuery( name, value.ToString() );

	public UriBuilder RemoveQuery( string name )
	{
		this._query.Remove( name );
		base.Query = this.Query;

		return this;
	}	

	public UriBuilder ReplaceQuery( string name, string value )
	{
		return this.RemoveQuery( name ).AddQuery( name, value );
	}	

	public UriBuilder ReplaceQuery( string name, int value ) => ReplaceQuery( name, value.ToString() );
	public UriBuilder ReplaceQuery( string name, double value ) => ReplaceQuery( name, value.ToString() );
	public UriBuilder ReplaceQuery( string name, float value ) => ReplaceQuery( name, value.ToString() );

}

static public class UriBuilderRequestExtension 
{
	static public UriBuilder GetUriBuilder( this HttpRequest request )
	{
		return new UriBuilder( request.GetDisplayUrl() );
	}
}


