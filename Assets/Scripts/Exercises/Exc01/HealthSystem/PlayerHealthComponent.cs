using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthComponent : AHealthComponent
{
    [SerializeField] private Slider _healthSlider;

    protected override void Awake()
    {
        base.Awake();
        _healthSlider.maxValue = Health.Value;
        _healthSlider.value = Health.Value;
    }

    protected override void OnHealthChanged(int prev, int next, bool asServer)
    {
        _healthSlider.value = Health.Value;
    }
}
