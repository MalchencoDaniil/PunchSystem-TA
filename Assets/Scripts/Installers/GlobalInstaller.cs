using Zenject;
using UnityEngine;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private InputService _inputServicePrefab;

    public override void InstallBindings()
    {
        Container.Bind<InputService>().FromComponentInNewPrefab(_inputServicePrefab).AsSingle();
    }
}