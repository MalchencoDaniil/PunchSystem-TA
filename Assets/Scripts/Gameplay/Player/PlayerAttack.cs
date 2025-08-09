using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput _playerInput;
    private float _timeToAttack = 0;

    [SerializeField] private PlayerData _playerData;

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
        if (_timeToAttack <= 0f)
        {
            if (_playerInput.LeftPunch())
            {
                Punch();
            }

            if (_playerInput.RightPunch())
            {
                Punch();
            }
        }
    }

    private void Punch()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit _raycastHit))
        {
            IHit _hittable = _raycastHit.collider.GetComponent<IHit>();

            if (_hittable != null)
            {
                _playerAudioSource.PlayOneShot(_punchClip);
                _hittable.TakeHit(_playerData.Damage, _raycastHit.point);
                _timeToAttack = _playerData.AttackReloadTime;
                return;
            }
        }

        _playerAudioSource.PlayOneShot(_swingClip);
        _timeToAttack = _playerData.AttackReloadTime;
    }
}