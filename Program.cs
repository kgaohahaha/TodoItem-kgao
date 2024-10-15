using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Api;
using ToDoList.Api.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITodoItemService, ToDoListItemsService>();
builder.Services.Configure<TodoItemDBSetting>(builder.Configuration.GetSection("ToDoItemDatabase"));

builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
{
    var settings = ServiceProvider.GetRequiredService<IOptions<TodoItemDBSetting>>().Value;
    return new MongoClient(settings.ConnectionString);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
