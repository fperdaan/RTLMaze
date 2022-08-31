using RTLMaze.REST.Startup;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

# region Start configuration

var mvcBuilder = builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// use lowercase urls
builder.Services.AddRouting( options => options.LowercaseUrls = true );

// -- Configure swagger
builder.Services.ConfigureOptions<SwaggerVersionProvider>();
builder.Services.AddSwaggerGen( c => 
{
	c.DocumentFilter<SwaggerLatestSchemeFilter>(); 
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
