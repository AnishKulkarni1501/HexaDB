using Grpc.Core;
using KeyValueDb.Storage;
using System.Net;

namespace KeyValueDb.Services;

public class KeyValueGrpcService : KeyValueService.KeyValueServiceBase
{
    private readonly KeyValueStore _store;

    public KeyValueGrpcService(KeyValueStore store)
    {
        _store = store;
    }

    public override Task<PutResponse> Put(
        PutRequest request,
        ServerCallContext context)
    {
        _store.Put(request.Key, request.Value.ToByteArray());

        return Task.FromResult(new PutResponse
        {
            Success = true
        });
    }

    public override Task<GetResponse> Get(
        GetRequest request,
        ServerCallContext context)
    {
        if (_store.TryGet(request.Key, out var value))
        {
            return Task.FromResult(new GetResponse
            {
                Found = true,
                Value = Google.Protobuf.ByteString.CopyFrom(value!)
            });
        }

        return Task.FromResult(new GetResponse
        {
            Found = false
        });
    }

    public override Task<DeleteResponse> Delete(
        DeleteRequest request,
        ServerCallContext context)
    {
        bool deleted = _store.Delete(request.Key);

        return Task.FromResult(new DeleteResponse
        {
            Success = deleted
        });
    }
}