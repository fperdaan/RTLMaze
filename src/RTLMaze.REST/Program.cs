global using RTLMaze.REST.Models.Responses;

using RTLMaze.REST.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

# region Start configuration

var mvcBuilder = builder.Services.AddControllers();

mvcBuilder.AddJsonOptions( options => 
{
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

	options.JsonSerializerOptions.Converters.Add( new DateOnlySerializer() );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// use lowercase urls
builder.Services.AddRouting( options => options.LowercaseUrls = true );

// -- Configure swagger
builder.Services.ConfigureOptions<SwaggerVersionProvider>();
builder.Services.AddSwaggerGen( c => 
{
	c.DocumentFilter<SwaggerLatestSchemeFilter>(); 
	c.MapType<DateOnly>(() => new OpenApiSchema { 
		Type = "string",
		Pattern = DateOnlySerializer.DATE_FORMAT,
		Example = new OpenApiString( DateOnly.FromDateTime( DateTime.Now ).ToString( DateOnlySerializer.DATE_FORMAT ) )
	});
});

// -- Configure versioning api
builder.Services.AddApiVersioning( options =>
{
	options.DefaultApiVersion = new ApiVersion( 1, 0 );
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer( setup =>
{
	setup.GroupNameFormat = "'v'VVV";
	setup.SubstituteApiVersionInUrl = true;
});

// Overload default error response with our model
builder.Services.Configure<ApiBehaviorOptions>( options => 
{
    options.InvalidModelStateResponseFactory = context => new ResponseError<bool>( new ValidationProblemDetails( context.ModelState ) ).Convert();
});

// -- Configure cors ( enable all for now )
builder.Services.AddCors( c => c.AddDefaultPolicy( b => 
{
	b.AllowAnyOrigin()
	 .AllowAnyMethod()
	 .AllowAnyHeader();
} ) );

// Set database context
builder.Services
			.AddDbContext<RTLMaze.DAL.RTLMazeStorageContext>( 
				c => c.UseSqlite( 
					builder.Configuration.GetConnectionString("DefaultConnection"), 
					b => b.MigrationsAssembly("RTLMaze.REST") 
				) );

// -- Configure solutions
RTLMaze.DAL.Configure.ConfigureServices( builder.Services );

# endregion

# region Build app 

var app = builder.Build();

// Configure the HTTP request pipeline.

// -- Add swagger
app.UseSwagger();
app.UseSwaggerUI( o => 
{
	var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

	// Build a swagger endpoint for each discovered API version  
	foreach ( var description in apiVersionDescriptionProvider.ApiVersionDescriptions )  
	{  
		o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant() );  
	} 
});

// -- Enable cors
app.UseCors();

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

# endregion

app.Run();
