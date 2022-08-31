using System.Net.Http.Formatting;
using System.Text.Json;
using Newtonsoft.Json;

using RTLMaze.Core.Extensions;
using RTLMaze.Models;

// -- Temp used for reading data
JsonConvert.DefaultSettings = () => new JsonSerializerSettings{ Formatting = Formatting.Indented, Converters = { new DateOnlySerializer(), new DateOnlySerializerNullable() } };

// For some funny reason this uses newtonsoft which is incompetible with our default converter from the rest project
var formatters = new MediaTypeFormatterCollection();
var jsonFormatter = formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
jsonFormatter?.SerializerSettings.Converters.Add( new DateOnlySerializer() );
jsonFormatter?.SerializerSettings.Converters.Add( new DateOnlySerializerNullable() );

HttpClient client = new HttpClient();
HttpResponseMessage response = await client.GetAsync( "https://api.tvmaze.com/shows/1" );

if ( response.IsSuccessStatusCode )
{
	var obj = await response.Content.ReadAsAsync<Title>( formatters );

	Console.WriteLine( obj.Name );
	Console.WriteLine( JsonConvert.SerializeObject( obj ) );

}
else 
{
	Console.WriteLine("Something went wrong");
}
