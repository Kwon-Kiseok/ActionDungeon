using Zenject;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawner
{
    private GameObject _enemyInstance;
    public GameObject EnemyInstance => _enemyInstance;

    public Enemy SpawnNewEnemy(string enemyKey, Transform spawnParentTransform)
    {
        Enemy enemy = new Enemy();
        var enemyInstance = Addressables.InstantiateAsync(enemyKey, spawnParentTransform).WaitForCompletion();
        _enemyInstance = enemyInstance;
        enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Init(enemy.unitName, new Units.statData(10, 3, 1, 0.5f));
        return enemy;
    }
}
