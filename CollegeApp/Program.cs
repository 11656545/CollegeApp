using CollegeApp.MyLogging;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//This line will create an object for the class(LogToFile, LogToDB,LogToServermemory etc)when someone calls IMylogger 
builder.Services.AddScoped<IMyLogger, LogToFile>();
//builder.Services.AddScoped<IMyLogger, LogToDB>();
//builder.Services.AddScoped<IMyLogger, LogToServerMemory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
