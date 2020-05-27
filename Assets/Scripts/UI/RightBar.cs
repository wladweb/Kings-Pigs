using UnityEngine;
using UnityEngine.UI;

public class RightBar : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private Button _openShop;

    private void OnEnable()
    {
        _openShop.onClick.AddListener(OnShopButtonClick);
    }

    private void OnDisable()
    {
        _openShop.onClick.RemoveListener(OnShopButtonClick);
    }

    private void OnShopButtonClick()
    {
        _shop.gameObject.SetActive(true);
    }
}
