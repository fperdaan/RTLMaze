using System.Text.Json;
using System.Text.Json.Nodes;
using RTLMaze.Core;
using RTLMaze.Core.Scraper;
using RTLMaze.Models;

// -- Temp used for reading data

var jsonOptions = new JsonSerializerOptions { 
	PropertyNameCaseInsensitive = true,
	WriteIndented = true,
	Converters = { 
		new DateOnlySerializer()
	}	
};



var source = new FileStream( "Local/update-shows.json", FileMode.Open );

// var result = new JsonStreamProcessor<List<Title>>()
// 				.SetJsonOptions( jsonOptions )
// 				.Process( source );

// var source = new HttpSource()
// 				.FromUrl("https://api.tvmaze.com/updates/shows");

var result = new JsonStreamProcessor<Dictionary<string, int>>()
				.SetJsonOptions( jsonOptions )
				.Process( source );

var timestamp = new DateTimeOffset( DateTime.UtcNow ).AddDays( -7 ).ToUnixTimeSeconds();
var updated = result
				.Where( kv => kv.Value >= timestamp )
				.Select( kv => Int32.Parse( kv.Key ) )
				.ToList();

Console.WriteLine( updated.Count() );

foreach( int id in updated )
{
	var url = $"https://api.tvmaze.com/shows/{id}?embed=cast";

	var source2 = new FileStream( "Local/show.json", FileMode.Open );

	var result2 = new JsonStreamProcessor<Title>()
				.SetJsonOptions( jsonOptions )
				.Process( source2 );

	Console.WriteLine( JsonSerializer.Serialize( result2, jsonOptions ) );
	break;
}

// var source = new FileStream( "Local/person.json", FileMode.Open );

// // var source = new HttpSource()
// // 				.FromUrl("https://api.tvmaze.com/updates/shows/1?embed=cast")
// // 	 			.SetMaxRequestAttempts( 10 )
// //				.GetSource();


// var result = new JsonStreamProcessor<Country>()
// 				.SetJsonOptions( jsonOptions )
// 				.Process( source );

// Console.WriteLine( JsonSerializer.Serialize( result, jsonOptions ) );
