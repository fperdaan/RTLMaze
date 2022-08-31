using System.Collections.Specialized;
using System.Web;

namespace RTLMaze.REST.Models;

public partial class UriBuilder : System.UriBuilder
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

	public virtual UriBuilder AddQuery( string name, string value )
	{
		this._query.Add( name, value );
		base.Query = this.Query;

		return this;
	}
	public virtual UriBuilder AddQuery( string name, int value ) => AddQuery( name, value.ToString() );
	public virtual UriBuilder AddQuery( string name, double value ) => AddQuery( name, value.ToString() );
	public virtual UriBuilder AddQuery( string name, float value ) => AddQuery( name, value.ToString() );

	public virtual UriBuilder RemoveQuery( string name )
	{
		this._query.Remove( name );
		base.Query = this.Query;

		return this;
	}	

	public virtual UriBuilder ReplaceQuery( string name, string value )
	{
		return this.RemoveQuery( name ).AddQuery( name, value );
	}	

	public virtual UriBuilder ReplaceQuery( string name, int value ) => ReplaceQuery( name, value.ToString() );
	public virtual UriBuilder ReplaceQuery( string name, double value ) => ReplaceQuery( name, value.ToString() );
	public virtual UriBuilder ReplaceQuery( string name, float value ) => ReplaceQuery( name, value.ToString() );

}