using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    private GameResultController _gameController;

    [Header("Player")]
    [SerializeField] private PlayerAttack _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    private PlayerAttack _playerInstance;

    [Header("Enemy")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _enemySpawnPoint;

    [Space(15)]
    [SerializeField] private HealthUI _enemyHealthUI;
    [SerializeField] private Text _enemyNameText;

    private Enemy _enemyInstance;

    private BlackScreen _blackScreen;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        _blackScreen = FindObjectOfType<BlackScreen>();

        PlayerBind();
        EnemyBind();

        CameraInit();

        _enemyHealthUI.Initialize(_enemyInstance.HealthSystem);
        _enemyNameText.text = _enemyInstance.Name;

        _gameController = FindObjectOfType<GameResultController>();
        _enemyInstance.HealthSystem.OnDeath.AddListener(_gameController.Won);

        _blackScreen.CloseBlackScreen();
    }

    private void OnDestroy()
    {
        _enemyInstance.HealthSystem.OnDeath.RemoveListener(_gameController.Won);
    }

    private void CameraInit()
    {
        CinemachineVirtualCamera _virtualCamera = _playerInstance.GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.Follow = _enemyInstance.transform;
        _virtualCamera.m_LookAt = _enemyInstance.transform;
    }

    private void PlayerBind()
    {
        _container.Bind<PlayerInput>().AsSingle();

        _playerInstance = _container.InstantiatePrefabForComponent<PlayerAttack>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity,null);
        _container.Bind<PlayerAttack>().FromInstance(_playerInstance).AsSingle();
    }

    private void EnemyBind()
    {
        _enemyInstance = _container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab, _enemySpawnPoint.position, Quaternion.identity, null);
        _container.Bind<Enemy>().FromInstance(_enemyInstance).AsSingle();
    }
}