using System.Text.Json;
using System.Text.Json.Serialization;
using RTLMaze.Core;
using RTLMaze.Core.Scraper;
using RTLMaze.Models;

// -- Temp used for reading data
	
var jsonOptions = new JsonSerializerOptions { 
	PropertyNameCaseInsensitive = true,
	WriteIndented = true,
	Converters = { 
		new DateOnlySerializer(), 
		new JsonStringEnumConverter()
	}	
};



var source = new FileStream( "Local/shows.json", FileMode.Open );

var result = new JsonStreamProcessor<List<Title>>()
				.Process( source );

Console.WriteLine( result.Count() );

// var source = new HttpSource()
// 				.FromUrl("https://api.tvmaze.com/updates/shows");

// var result = new JsonStreamProcessor<Dictionary<string, int>>()
// 				.Process( source );

// var timestamp = new DateTimeOffset( DateTime.UtcNow ).AddDays( -7 ).ToUnixTimeSeconds();
// var updated = result
// 				.Where( kv => kv.Value >= timestamp )
// 				.Select( kv => Int32.Parse( kv.Key ) )
// 				.ToList();

// Console.WriteLine( updated.Count() );

// var source = new FileStream( "Local/person.json", FileMode.Open );

// // var source = new HttpSource()
// // 				.FromUrl("https://api.tvmaze.com/updates/shows/1?embed=cast")
// // 	 			.SetMaxRequestAttempts( 10 )
// //				.GetSource();


// var result = new JsonStreamProcessor<Country>()
// 				.SetJsonOptions( jsonOptions )
// 				.Process( source );

// Console.WriteLine( JsonSerializer.Serialize( result, jsonOptions ) );
