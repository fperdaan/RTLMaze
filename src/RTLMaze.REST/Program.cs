using System.Web.Http.Cors;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

# region Start configuration

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// -- Configure swagger
builder.Services.AddSwaggerGen();

// -- Configure cors ( enable all for now )
builder.Services.AddCors( c => c.AddDefaultPolicy( b => 
{
	b.AllowAnyOrigin()
	 .AllowAnyMethod()
	 .AllowAnyHeader();
} ) );

# endregion

# region Build app 

var app = builder.Build();


// Configure cors 
// -- Whitelist all for now ( by default )
var cors = new EnableCorsAttribute( "*", "*", "*" );

// Configure the HTTP request pipeline.

// -- Add swagger
app.UseSwagger();
app.UseSwaggerUI();

// -- Enable cors
app.UseCors();

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

# endregion

app.Run();
