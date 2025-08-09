using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("References")]
    public string Name;
    [field: SerializeField] public Animator EnemyAnimator;
    [field: SerializeField] public HealthSystem HealthSystem;

    public virtual void Death() { }
}