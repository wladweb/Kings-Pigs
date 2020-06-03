using UnityEngine;

public class BoxItemHolder : ObjectPool
{
    [SerializeField] private GameObject[] _goods;

    private void Start()
    {
        Initialize(_goods);
    }

    public GameObject TryGetItem()
    {
        return TryGetObject(out GameObject item) ? item : null;
    }
}
