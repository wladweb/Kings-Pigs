using UnityEngine;

public class ThrowedBoxes : ItemHolder
{
    [SerializeField] private GameObject _throwedBox;

    protected override GameObject Template => _throwedBox;
}
