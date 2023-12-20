using SampleRedisandSignalR.RedisBase.Base;
using SampleRedisandSignalR.RedisBase.Logic;
using SampleRedisandSignalR.SignalRBase.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<RedisConnector>();
builder.Services.AddSingleton<IIndexManager, IndexManager>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<MessageHub>("/chatHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
