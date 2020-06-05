using UnityEngine;

[RequireComponent(typeof(BoxDestroyable))]
public class BoxThrowable : MonoBehaviour
{
    [SerializeField] private int _damage = 100;

    private BoxDestroyable _boxDestroyable;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _boxDestroyable = GetComponent<BoxDestroyable>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<King>(out King king))
        {
            king.ApplyDamage(_damage);
        }

        _boxDestroyable.DestroyBox();
    }
}
