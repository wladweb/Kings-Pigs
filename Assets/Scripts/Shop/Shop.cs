using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Hammer[] _hammers;
    [SerializeField] private Transform _container;
    [SerializeField] private HammerView _template;
    [SerializeField] private King _king;

    private void Start()
    {
        foreach (Hammer hammer in _hammers)
        {
            AddHammer(hammer);
        }
    }

    private void AddHammer(Hammer hammer)
    {
        HammerView hammerView = Instantiate(_template, _container.transform);
        hammerView.SellButtonClicked += OnSellButtonClick;
        hammerView.Render(hammer);
    }

    private void OnSellButtonClick(Hammer hammer, HammerView view)
    {
        TrySellHammer(hammer, view);
    }

    private void TrySellHammer(Hammer hammer, HammerView view)
    {
        if (_king.Diamonds >= hammer.Price)
        {
            _king.BuyingHammer(hammer);
            view.Deactivate();
            view.SellButtonClicked -= OnSellButtonClick;
        }
    }
}
