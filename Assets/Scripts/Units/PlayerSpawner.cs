using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerSpawner
{
    public Player SpawnPlayer(Transform spawnParentTransform)
    {
        var playerInstance = Addressables.InstantiateAsync("Player/Player_Object", spawnParentTransform).WaitForCompletion();
        Player player = playerInstance.GetComponent<Player>();
        player.Init(new Units.statData("Player", 10, 3, 1, 0.5f));

        return player;
    }
}
