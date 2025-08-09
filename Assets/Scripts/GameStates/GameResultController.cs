using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameResultController : Game
{
    private SceneService _sceneService;
    private BlackScreen _blackScreen;

    [SerializeField] private float _closeTime = 2;

    private void Awake()
    {
        _sceneService = FindObjectOfType<SceneService>();
        _blackScreen = FindObjectOfType<BlackScreen>();
    }

    public override void Won()
    {
        WonTaskAsync(_closeTime).Forget();
    }

    private async UniTaskVoid WonTaskAsync(float timeToWait)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(timeToWait));

        _blackScreen.OpenBlackScreen();

        await UniTask.Delay(TimeSpan.FromSeconds(_blackScreen.Duration));

        _sceneService.Restart();
    }

    public override void Loss()
    {
        base.Loss();
    }
}