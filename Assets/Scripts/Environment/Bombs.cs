using UnityEngine;

public class Bombs : ObjectPool
{
    [SerializeField] private GameObject _bombTemplate;
    [SerializeField] private Blows _blows;

    private void Awake()
    {
        Initialize(_bombTemplate);
    }

    public GameObject GetBomb()
    {
        if (TryGetObject(out GameObject bomb))
            return bomb;
        else
            return null;
    }

    private void Update()
    {
        DisableObjectsAbroadScreen();
    }
}
