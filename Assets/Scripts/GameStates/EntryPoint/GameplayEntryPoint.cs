using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    private BlackScreen _blackScreen;

    private void Awake()
    {
        _blackScreen = FindObjectOfType<BlackScreen>();
        _blackScreen.CloseBlackScreen();
    }
}