using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAssetProvider
{
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    void Release(AssetReference assetReference);
}

public class AssetProvider : IAssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> cache = new();

    public async Task<T> Load<T>(AssetReference assetReference) where T : class
    {
        string address = assetReference.AssetGUID;

        if (cache.TryGetValue(address, out var cachedHandle))
            return cachedHandle.Result as T;

        var handle = Addressables.LoadAssetAsync<T>(address);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cache[address] = handle;
            return handle.Result;
        }

        Debug.LogError($"Failed to upload asset at: {address}");
        return null;

    }

    public void Release(AssetReference assetReference)
    {
        if (cache.TryGetValue(assetReference.AssetGUID, out var handle))
        {
            Addressables.Release(handle);
            cache.Remove(assetReference.AssetGUID);
        }
    }
}
