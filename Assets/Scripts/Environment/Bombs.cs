using UnityEngine;

public class Bombs : ItemHolder
{
    [SerializeField] private GameObject _bombTemplate;

    protected override GameObject Template => _bombTemplate;
}
