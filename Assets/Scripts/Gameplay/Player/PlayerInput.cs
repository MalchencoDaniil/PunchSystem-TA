using UnityEngine;
using Zenject;

public class PlayerInput
{
    private InputService _inputService;

    [Inject]
    public void Construct(InputService inputService)
    {
        _inputService = inputService;
        Debug.Log("PlayerInput " + _inputService);
    }

    public bool LeftPunch()
    {
        return _inputService.InputActions.Player.LeftPunch.triggered;
    }

    public bool RightPunch()
    {
        return _inputService.InputActions.Player.RightPunch.triggered;
    }
}