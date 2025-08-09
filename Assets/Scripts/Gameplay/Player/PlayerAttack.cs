using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput _playerInput;
    private float _timeToAttack = 0;

    [SerializeField] private PlayerData _playerData;

    [Header("Arms")]
    [SerializeField] private GloveHitDOTween _leftArm;
    [SerializeField] private GloveHitDOTween _rightArm;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip _punchClip;
    [SerializeField] private AudioClip _swingClip;
    [SerializeField] private AudioSource _playerAudioSource;

    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    private void Awake()
    {
        _timeToAttack = _playerData.AttackReloadTime;
    }

    private void Update()
    {
        _timeToAttack -= Time.deltaTime;

        ProcessPunchInput();
    }

    private void ProcessPunchInput()
    {
        if (_timeToAttack > 0f) return;

        if (TryGetHitPoint(out Vector3 hitPoint))
        {
            if (_playerInput.LeftPunch())
            {
                _leftArm.Hit(hitPoint);
                DoPunch(hitPoint);
            }
            else if (_playerInput.RightPunch())
            {
                _rightArm.Hit(hitPoint);
                DoPunch(hitPoint);
            }
        }
        else
        {
            if (_playerInput.LeftPunch())
            {
                _leftArm.Hit(transform.position);
                DoSwing();
            }
            else if (_playerInput.RightPunch())
            {
                _rightArm.Hit(transform.position);
                DoSwing();
            }
        }
    }

    private bool TryGetHitPoint(out Vector3 _point)
    {
        _point = Vector3.zero;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit))
        {
            _point = hit.point;
            return true;
        }

        return false;
    }

    private void DoPunch(Vector3 hitPoint)
    {
        _playerAudioSource.PlayOneShot(_punchClip);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            var _hittable = hit.collider.GetComponent<IHit>();

            if (_hittable != null)
            {
                _hittable.TakeHit(_playerData.Damage, hitPoint);
            }
        }

        _timeToAttack = _playerData.AttackReloadTime;
    }

    private void DoSwing()
    {
        _playerAudioSource.PlayOneShot(_swingClip);
        _timeToAttack = _playerData.AttackReloadTime;
    }
}