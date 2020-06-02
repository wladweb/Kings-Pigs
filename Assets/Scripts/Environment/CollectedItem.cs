using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public abstract class CollectedItem : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _collectSound;

    public event UnityAction ItemCollected;

    private void OnEnable()
    {
        ItemCollected += ItemCollect;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collectSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        if (collision.gameObject.TryGetComponent<King>(out King king))
        {
            ChargeItem(king, 1);
            ItemCollected?.Invoke();
        }
    }

    private void ItemCollect()
    {
        _animator.SetTrigger("Collected");
        _collectSound.Play();
    }

    private void EraseItem()
    {
        ItemCollected -= ItemCollect;
        Destroy(gameObject);
    }

    protected abstract void ChargeItem(King king, int count);
}
