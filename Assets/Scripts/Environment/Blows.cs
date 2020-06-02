using UnityEngine;

public class Blows : ObjectPool
{
    [SerializeField] private GameObject _blowTemplate;

    private void Awake()
    {
        Initialize(_blowTemplate);
    }

    public GameObject GetBlow()
    {
        if (TryGetObject(out GameObject blow))
            return blow;
        else
            return null;
    }
}
