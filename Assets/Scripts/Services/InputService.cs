using UnityEngine;

public class InputService : MonoBehaviour
{
    public InputActions InputActions;

    private void Awake()
    {
        InputActions = new InputActions();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}