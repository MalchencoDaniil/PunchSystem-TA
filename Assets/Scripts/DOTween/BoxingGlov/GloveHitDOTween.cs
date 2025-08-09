using DG.Tweening;
using UnityEngine;

public class GloveHitDOTween : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _startPoint;

    [Header("Movement")]
    [SerializeField] private float moveDuration = 0.12f;
    [SerializeField] private float returnDuration = 0.15f;
    [SerializeField] private float hitDistanceOffset = 0.2f;

    [Header("Punch Rotation")]
    [SerializeField] private float punchRotationX = -45f;
    [SerializeField] private float punchElasticity = 0.8f;

    private Sequence _sequence;

    public void Hit(Vector3 _targetPosition)
    {
        _sequence?.Kill();

        Vector3 _directionToGlove = (_startPoint.position - _targetPosition).normalized;
        Vector3 _hitPosition = _targetPosition + _directionToGlove * hitDistanceOffset;

        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOMove(_hitPosition, moveDuration).SetEase(Ease.OutQuad));

        Vector3 _punchRotation = new Vector3(punchRotationX, transform.rotation.y, transform.rotation.z);

        _sequence.Append(transform.DOMove(_hitPosition, moveDuration).SetEase(Ease.OutQuad));
        _sequence.Join(transform.DOLocalRotate(_punchRotation, moveDuration * 0.6f, RotateMode.Fast).SetRelative(true).SetEase(Ease.OutQuad));
        _sequence.AppendInterval(0.02f);

        _sequence.Append(transform.DOMove(_startPoint.position, returnDuration).SetEase(Ease.OutQuad));
        _sequence.Join(transform.DORotateQuaternion(_startPoint.rotation, returnDuration).SetEase(Ease.OutQuad));

        _sequence.OnComplete(() => _sequence = null);
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}