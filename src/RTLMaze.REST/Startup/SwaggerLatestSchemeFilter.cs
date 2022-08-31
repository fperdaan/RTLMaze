
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RTLMaze.REST.Startup;

/// <summary>
/// Filters out the request calls to the /latest/ schemes from our swagger UI. 
/// </summary>
public class SwaggerLatestSchemeFilter : IDocumentFilter
{
	public void Apply( OpenApiDocument swaggerDoc, DocumentFilterContext context )
	{
		var remove = swaggerDoc.Paths
						.Where( kp => kp.Key.Contains("api/latest") )
						.Select( kp => kp.Key )
						.ToList();

		foreach( string key in remove )
		{
			swaggerDoc.Paths.Remove( key );
		}
	}
}