var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

# region Start configuration

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// -- Configure swagger
builder.Services.AddSwaggerGen();

# endregion

# region Build app 

var app = builder.Build();

// Configure the HTTP request pipeline.

// -- Add swagger
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

# endregion

app.Run();
