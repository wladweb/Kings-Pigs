using UnityEngine;
using UnityEngine.Events;

public class BoxItem : MonoBehaviour
{
    [SerializeField] private int _hitsCountHealth;
    [SerializeField] private int _goodsCount;
    [SerializeField] private float _spread;
    [SerializeField] Vector2 _goodsPushDirection;
    [SerializeField] private ParticleSystem _fog;

    private BoxItemHolder _boxHolder;

    public event UnityAction<float> BoxDestroyed;

    private void Awake()
    {
        _boxHolder = transform.parent.GetComponent<BoxItemHolder>();
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
        ComponentsHandle();
        PushGoods(kingPositionX);
        HideBox();
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

    private void HideBox()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Destroy(gameObject, 1f);
    }

    private void ComponentsHandle()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<AudioSource>().Play();
        _fog.Play();
    }
}
