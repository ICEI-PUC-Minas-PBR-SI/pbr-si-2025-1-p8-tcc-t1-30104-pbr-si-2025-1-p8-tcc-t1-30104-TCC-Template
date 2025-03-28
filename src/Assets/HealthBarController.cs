using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    public GameObject _cart;

    private HealthSystem _healthSystemCart;

    private Image _image;

    void Start()
    {
        _healthSystemCart = _cart.GetComponent<HealthSystem>();
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (_healthSystemCart != null)
        {
            _image.fillAmount = (_healthSystemCart.health / _healthSystemCart.maxHealth);
        }
    }
}
