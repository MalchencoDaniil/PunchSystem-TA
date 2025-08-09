using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(VisualHit))]
public class Zombie : Enemy, IHit
{
    private VisualHit _visualHit;

    [Header("Particles")]
    [SerializeField] private Transform _hitParticle;
    [SerializeField] private Transform _deathParticle;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioSource _enemyAudioSourcePrefab;

    private void Start()
    {
        _visualHit = GetComponent<VisualHit>();
    }

    public override void Death()
    {
        EnemyAnimator.SetTrigger("Death");
        DeathAsyncVoid().Forget();
    }

    public async UniTaskVoid DeathAsyncVoid()
    {
        await UniTask.WaitForSeconds(1);

        AudioSource _newAudioSource = Instantiate(_enemyAudioSourcePrefab);
        _newAudioSource.PlayOneShot(_explosionSound);

        Transform _newExplosion = Instantiate(_deathParticle, transform.position, Quaternion.identity);
        Destroy(_newExplosion.gameObject, 0.5f);

        Destroy(gameObject);
    }

    public void TakeHit(float _damage, Vector3 _hitPosition)
    {
        EnemyAnimator.SetFloat("HitID", Random.Range(0, 3));
        EnemyAnimator.SetTrigger("Hit");

        _visualHit.Hit();
        HealthSystem.TakeDamage(_damage);

        Transform _newExplosion = Instantiate(_hitParticle, _hitPosition, Quaternion.identity);
        Destroy(_newExplosion.gameObject, 0.5f);
    }
}