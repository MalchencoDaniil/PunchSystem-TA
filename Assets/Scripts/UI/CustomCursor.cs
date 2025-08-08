using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private RectTransform _cursorRect;
    [SerializeField] private Canvas _canvas;

    private void Update()
    {
        Vector2 _screenPosition = Input.mousePosition;
        Vector2 _localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(), _screenPosition, _canvas.worldCamera, out _localPoint))
        {
            _cursorRect.localPosition = _localPoint;
        }
    }
}