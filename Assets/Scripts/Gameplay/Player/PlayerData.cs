using UnityEngine;

[CreateAssetMenu(menuName = "Player/Data", fileName = "New Player Data")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float Damage = 5;
    [field: SerializeField] public float AttackSpeed = 10;
    [field: SerializeField, Range(0.1f, 10f)] public float AttackReloadTime = 1;
}