using DataStorageAPI.DataAccess;
using DataStorageAPI.DataAccess.Interfaces;
using DataStorageAPI.DataStore;
using DataStorageAPI.Middlewares;
using DataStorageAPI.ServiceLayer;
using DataStorageAPI.ServiceLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataStore,InMemoryDataStore>();
builder.Services.AddSingleton<IDataObjectRepository,DataObjectRepository>();
builder.Services.AddSingleton<IRepoRepository, RepoRepository>();
builder.Services.AddSingleton<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton<IDataObjectService, DataObjectService>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthorization();
app.UseEndpoints((endpoints) => 
{
    endpoints.MapControllers();
});
app.Run();
