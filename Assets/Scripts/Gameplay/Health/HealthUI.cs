using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Slider _healthSlider;

    [SerializeField] private Text _healthText;

    public void Initialize(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        _healthSystem.OnTakeDamage.AddListener(CanvasUpateHealth);
    }

    private void Start()
    {
        if (_healthSystem == null)
            return;

        _healthSlider = GetComponent<Slider>();

        _healthText.text = _healthSystem.CurrentHealth.ToString();
        _healthSlider.maxValue = _healthSystem.CurrentHealth;
        _healthSlider.value = _healthSystem.CurrentHealth;

        CanvasUpateHealth();
    }

    private void OnDestroy()
    {
        if (_healthSystem != null)
        {
            _healthSystem.OnTakeDamage.RemoveListener(CanvasUpateHealth);
        }
    }

    public void CanvasUpateHealth()
    {
        _healthText.text = _healthSystem.CurrentHealth.ToString();
        _healthSlider.value = _healthSystem.CurrentHealth;
    }
}