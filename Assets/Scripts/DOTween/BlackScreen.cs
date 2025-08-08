using Cysharp.Threading.Tasks;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private float _durationTime = 0.8f;

    public float Duration => _durationTime;

    public void OpenBlackScreen() => Open(_durationTime).Forget();
    public void CloseBlackScreen() => Close(_durationTime).Forget();

    private async UniTaskVoid Open(float _time)
    {
        _popup.gameObject.SetActive(true);

        _popup.Show(_time);

        await UniTask.WaitForSeconds(_time);
    }

    private async UniTaskVoid Close(float _time)
    {
        _popup.Hide(_time);

        await UniTask.WaitForSeconds(_time);

        _popup.gameObject.SetActive(false);
    }
}