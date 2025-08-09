using UnityEngine;

public class VisualHit : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Material _baseMaterial, _hitMaterial;
    [SerializeField, Range(0, 1)] private float _hitDuration;

    public void Hit()
    {
        _mesh.material = _hitMaterial;
        Invoke("ResetMaterial", _hitDuration);
    }

    private void ResetMaterial()
    {
        _mesh.material = _baseMaterial;
    }
}