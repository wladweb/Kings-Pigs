using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxDestroyable))]
public class BoxItem : MonoBehaviour
{
    [SerializeField] private int _hitsCountHealth;
    [SerializeField] private int _goodsCount;
    [SerializeField] private float _spread;
    [SerializeField] Vector2 _goodsPushDirection;

    private BoxItemHolder _boxHolder;
    private BoxDestroyable _boxDestroyable;
    private BoxCollider2D _collider;

    public event UnityAction<float> BoxDestroyed;

    private void Awake()
    {
        _boxHolder = transform.parent.GetComponent<BoxItemHolder>();
        _boxDestroyable = GetComponent<BoxDestroyable>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        BoxDestroyed += OnBoxDestroyed;
    }

    private void OnDisable()
    {
        BoxDestroyed -= OnBoxDestroyed;
    }

    public void ApplyDamage(float kingPositionX)
    {
        _hitsCountHealth--;

        if (_hitsCountHealth <= 0)
            BoxDestroyed?.Invoke(kingPositionX);
    }

    private void OnBoxDestroyed(float kingPositionX)
    {
        PushGoods(kingPositionX);
        _collider.enabled = false;
        _boxDestroyable.DestroyBox();
    }

    private void PushGoods(float kingPositionX)
    {
        for (int i = 1; i <= _goodsCount; i++)
        {
            GameObject item = _boxHolder.TryGetItem();

            if (item != null)
            {
                item.transform.position = transform.position;
                item.SetActive(true);
                item.GetComponent<Rigidbody2D>().AddForce(GetPushGoodsDirection(kingPositionX), ForceMode2D.Impulse);
            }
        }
    }

    private Vector2 GetPushGoodsDirection(float kingPositionX)
    {
        float directionX = _goodsPushDirection.x;
        float directionY = _goodsPushDirection.y;

        int kingSide = kingPositionX < transform.position.x ? 1 : -1;

        directionX *= Random.Range(2.2f, 3.3f);
        directionY *= Random.Range(1.3f, 1.5f);

        return new Vector2(directionX * kingSide, directionY);
    }
}
