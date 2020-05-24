using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Hammer[] _hammers;
    [SerializeField] private Transform _container;
    [SerializeField] private HammerView _template;

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
        hammerView.Render(hammer);
    }
}
