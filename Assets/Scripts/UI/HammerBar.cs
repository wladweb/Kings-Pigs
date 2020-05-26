using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HammerBar : MonoBehaviour
{
    [SerializeField] private Image _hammerIcon;
    [SerializeField] private TMP_Text _hammerName;
    [SerializeField] private King _king;

    private void OnEnable()
    {
        _king.HammerChanged += OnHammerChanged;
    }

    private void OnDisable()
    {
        _king.HammerChanged -= OnHammerChanged;
    }

    private void OnHammerChanged(Hammer hammer)
    {
        _hammerIcon.sprite = hammer.Icon;
        _hammerName.color = hammer.Color;
        _hammerName.text = hammer.HammerName;
    }
}
