using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameInitializer>().FromComponentInHierarchy().AsSingle();

        Container.Bind<EnemySpawner>().AsSingle();
        Container.Bind<PlayerSpawner>().AsSingle();

        InstallHierachyObjects();
    }

    private void InstallHierachyObjects()
    {
        Container.Bind<BattleManager>().FromComponentInHierarchy().AsSingle();
    }
}