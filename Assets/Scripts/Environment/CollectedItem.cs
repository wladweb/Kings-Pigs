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
        ItemCollected += OnItemCollect;
    }

    private void OnDisable()
    {
        ItemCollected -= OnItemCollect;
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
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (collision.gameObject.TryGetComponent<King>(out King king))
        {
            ChargeItem(king, 1);
            ItemCollected?.Invoke();
        }
    }

    private void OnItemCollect()
    {
        _animator.SetTrigger("Collected");
        _collectSound.Play();
    }

    private void EraseItem()
    {
        gameObject.SetActive(false);
    }

    protected abstract void ChargeItem(King king, int count);
}
