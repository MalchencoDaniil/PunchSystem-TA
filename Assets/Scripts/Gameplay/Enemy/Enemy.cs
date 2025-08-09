using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("References")]
    public string Name;
    [field: SerializeField] public Animator _enemyAnimator;
    [field: SerializeField] public HealthSystem _healthSystem;

    public virtual void Death() { }
}