using UnityEngine;

[RequireComponent(typeof(BoxDestroyable))]
public class BoxThrowable : MonoBehaviour
{
    [SerializeField] private int _damage = 100;

    private BoxDestroyable _boxDestroyable;

    private void Start()
    {
        _boxDestroyable = GetComponent<BoxDestroyable>();
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
