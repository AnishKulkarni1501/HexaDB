using Google.Protobuf;
using Grpc.Net.Client;
using KeyValueDb;

var channel = GrpcChannel.ForAddress("http://localhost:5133");

var client = new KeyValueService.KeyValueServiceClient(channel);

Console.WriteLine("Putting value...");

var putResponse = await client.PutAsync(
    new PutRequest
    {
        Key = "hello",
        Value = ByteString.CopyFromUtf8("world")
    });

Console.WriteLine($"PUT success: {putResponse.Success}");

Console.WriteLine("Getting value...");

var getResponse = await client.GetAsync(
    new GetRequest
    {
        Key = "hello"
    });

if (getResponse.Found)
{
    Console.WriteLine($"GET value: {getResponse.Value.ToStringUtf8()}");
}
else
{
    Console.WriteLine("Key not found.");
}