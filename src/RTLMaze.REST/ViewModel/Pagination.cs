using Microsoft.AspNetCore.Mvc;

namespace RTLMaze.REST.ViewModel;

public class Pagination
{
	const int MAX_RESULTS = 1000;

	private int _top = 10;

	[FromQuery(Name="$top")]
	public int Top { 
		get => _top; 
		set => _top = Math.Min( Math.Max( Math.Abs( value ), 1 ), MAX_RESULTS );
	}

	private int _skip = 0;

	[FromQuery(Name="$skip")]
	public int Skip { 
		get => _skip; 
		set => _skip = Math.Abs( value );
	}	
}

