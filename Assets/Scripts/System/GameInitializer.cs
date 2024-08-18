using UnityEngine;
using Zenject;

public class GameInitializer : MonoBehaviour
{
    private PlayerSpawner _playerSpawner;
    private Player _player;
    public Player Player { get { return _player; } }

    [SerializeField] private Transform playerSpawnPosition;

    [Inject]
    public void Inject(PlayerSpawner playerSpawner)
    {
        _playerSpawner = playerSpawner;
    }

    public void Start()
    {
        _player = _playerSpawner.SpawnPlayer(playerSpawnPosition);
    }
}
