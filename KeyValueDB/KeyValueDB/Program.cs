using KeyValueDb.Services;
using KeyValueDb.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddSingleton<KeyValueStore>();

var app = builder.Build();

app.MapGrpcService<KeyValueGrpcService>();

app.MapGet("/", () =>
    "KeyValueDb gRPC server is running.");

app.Run();