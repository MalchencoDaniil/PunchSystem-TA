using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(VisualHit))]
public class Zombie : Enemy, IHit
{
    private VisualHit _visualHit;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _hitParticle;
    [SerializeField] private ParticleSystem _deathParticle;

    public override void Death()
    {
        _enemyAnimator.SetTrigger("Death");
        DeathAsyncVoid().Forget();
    }

    public async UniTaskVoid DeathAsyncVoid()
    {
        await UniTask.WaitForSeconds(1);

        SpawnExplosion(transform.position, _deathParticle);
        Destroy(gameObject);
    }

    public void TakeHit(float _damage, Vector3 _hitPosition)
    {
        _enemyAnimator.SetFloat("HitID", Random.Range(0, 3));
        _enemyAnimator.SetTrigger("Hit");

        _visualHit.Hit();
        _healthSystem.TakeDamage(_damage);

        SpawnExplosion(_hitPosition, _hitParticle);
    }

    private void SpawnExplosion(Vector3 _position, ParticleSystem _explosion)
    {
        ParticleSystem _newExplosion = Instantiate(_explosion, _position, Quaternion.identity);
        Destroy(_newExplosion, _newExplosion.duration);
    }
}