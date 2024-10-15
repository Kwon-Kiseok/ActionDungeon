using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableSpriteLoader
{
    public static void SetSprite(Image targetImage, string path)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(path);
        handle.Completed += (op) => {
            targetImage.sprite = handle.Result;
        };

        Addressables.Release(handle);
    }
}
