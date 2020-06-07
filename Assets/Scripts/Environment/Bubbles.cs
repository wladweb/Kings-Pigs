using UnityEngine;

public class Bubbles : ItemHolder
{
    [SerializeField] private GameObject[] _bubbles;

    protected override GameObject Template => throw new System.NotImplementedException();

    private void Awake()
    {
        Initialize(_bubbles);
    }
}
