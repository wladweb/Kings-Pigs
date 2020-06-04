using UnityEngine;

public class CannonBalls : ItemHolder
{
    [SerializeField] private GameObject _ballTemplate;

    protected override GameObject Template
    {
        get
        {
            return _ballTemplate;
        }
    }
}
