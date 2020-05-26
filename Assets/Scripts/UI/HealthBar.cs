using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HealthBar : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _delay;
    [SerializeField] private King _king;

    public event UnityAction KingDied;

    private void Awake()
    {
        Initialize(_template);
        Reset();
    }

    private void OnEnable()
    {
        _king.HealthChanged += OnHealthChange;
    }

    private void OnDisable()
    {
        _king.HealthChanged -= OnHealthChange;
    }

    private void OnHealthChange(int heartsCount)
    {
        if (heartsCount < 0)
        {
            StartCoroutine(DecreaseHealth(Mathf.Abs(heartsCount)));
        }
        else
        {
            //to collect gems!!!!!
            for (int i = 1; i <= heartsCount; i++)
            {
                IncreaseHealth();
            }
        }
    }

    private void Reset()
    {
        StartCoroutine(CreateHearts());
    }

    private IEnumerator CreateHearts()
    {
        for (int i = 0; i < Capacity; i++)
        {
            if (TryGetObject(out GameObject result))
            {
                result.SetActive(true);
            }
            yield return new WaitForSeconds(_delay);
        }
    }

    private IEnumerator DecreaseHealth(int heartsCount)
    {
        if (TryGetLastActive(out GameObject lastActiveHeart))
        {
            for (int i = _pool.IndexOf(lastActiveHeart), l = i - heartsCount; i > l; i--)
            {
                _pool[i].GetComponent<Animator>().Play("Erase");

                if (i == 0)
                {
                    KingDied?.Invoke();
                    break;
                }

                yield return new WaitForSeconds(_delay);
            }
        }
    }

    private void IncreaseHealth()
    {
        if (TryGetObject(out GameObject firstInactiveHeart))
        {
            firstInactiveHeart.SetActive(true);
        }
    }
}
