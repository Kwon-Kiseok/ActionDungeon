using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameInitializer>().FromComponentInHierarchy().AsSingle();

        Container.Bind<EnemySpawner>().AsSingle();
        Container.Bind<PlayerSpawner>().AsSingle();

        Container.Bind<RandomDraftSystem>().AsSingle();

        InstallHierachyObjects();
        InstallDatabases();
    }

    private void InstallHierachyObjects()
    {
        Container.Bind<BattleManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TurnClockSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BattleReadyUI>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameProgressUI>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
    }

    private void InstallDatabases()
    {
        Container.Bind<EnhaceBonusDatabase>().AsSingle();
        Container.Bind<EnemyDatabase>().AsSingle();
    }
}