using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private Image _tank;

    private void OnEnable()
    {
        _health.HealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged += UpdateBar;
    }

    private void UpdateBar(float value)
    {
        _tank.fillAmount = value / _health.MaxHealth;
    }
}