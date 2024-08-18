using Zenject;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawner
{
    public Enemy SpawnNewEnemy(string enemyKey, Transform spawnParentTransform)
    {
        Enemy enemy = new Enemy();
        var enemyInstance = Addressables.InstantiateAsync(enemyKey, spawnParentTransform).WaitForCompletion();
        enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Init(enemy.unitName, new Units.statData(10, 3, 1, 0.5f));
        enemy.unitRenderer.GetUnitStatUI().SetStatus(enemy.GetStatData());
        return enemy;
    }

}
