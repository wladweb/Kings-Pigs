using UnityEngine;

public class Blows : ItemHolder
{
    [SerializeField] private GameObject _blowTemplate;

    protected override GameObject Template
    {
        get
        {
            return _blowTemplate;
        }
    }
}
