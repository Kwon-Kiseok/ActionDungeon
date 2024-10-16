using Zenject;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Text;

public class EnemySpawner
{
    private GameObject _enemyInstance;
    public GameObject EnemyInstance => _enemyInstance;

    private EnemyDatabase _enemyDatabase;

    private const string ENEMY_PREFAB_PATH = "Enemy/Enemy_";

    [Inject]
    public void Inject(EnemyDatabase enemyDatabase)
    {
        _enemyDatabase = enemyDatabase;
    }

    public void InitSpawner()
    {
        _enemyDatabase.Load();
    }

    public Enemy SpawnNewEnemy(Transform spawnParentTransform)
    {
        Enemy enemy = new Enemy();
        int randomEnemyID = _enemyDatabase.GetRandomEnemyID();
        Units.statData enemyData = _enemyDatabase.GetStatData(randomEnemyID);

        StringBuilder sb = new StringBuilder();
        string enemyKey = sb.Append(ENEMY_PREFAB_PATH).Append(enemyData.name).ToString();
        var enemyInstance = Addressables.InstantiateAsync(enemyKey, spawnParentTransform).WaitForCompletion();
        _enemyInstance = enemyInstance;
        enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Init(enemyData);
        return enemy;
    }
}
