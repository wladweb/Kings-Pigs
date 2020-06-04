using UnityEngine;

public class Bombs : ItemHolder
{
    [SerializeField] private GameObject _bombTemplate;

    protected override GameObject Template 
    {
        get
        {
            return _bombTemplate;
        }
    }
}
