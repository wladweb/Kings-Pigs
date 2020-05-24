using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HammerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _length;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    public event UnityAction SellButtonClicked;

    public void Render(Hammer hammer)
    {
        _name.text = hammer.HammerName;
        _name.color = hammer.Color;
        _damage.text = hammer.Damage.ToString();
        _speed.text = hammer.Speed.ToString();
        _length.text = hammer.Length.ToString();
        _price.text = hammer.Price.ToString();
        _icon.sprite = hammer.Icon;
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
    }

    private void OnSellButtonClick()
    {
        SellButtonClicked?.Invoke();
    }
}
