using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.UI;

 public class HealthBarFPSUI : NetworkBehaviour
{
    Slider _slider;
    private readonly SyncVar<float> _fillAmount = new ();
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _fillAmount.Value = _slider.value;
        _fillAmount.OnChange += OnHealthChanged;
    }

    public override void OnStartClient()
    {
        ActivateHealthBar();
    }

    //[ServerRpc(RequireOwnership = true)]
    private void ActivateHealthBar()
    {
        if (IsOwner)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    
    private void OnHealthChanged(float prev, float next, bool asServer)
    {
        _fillAmount.Value = next;
        _slider.value = _fillAmount.Value;
    }

    [ServerRpc (RequireOwnership = false)]
    public void UpdateFillAmount(float currentHealth)
    {
        _fillAmount.Value = currentHealth/ 100;
    }
}
