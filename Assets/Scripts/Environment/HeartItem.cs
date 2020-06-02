using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class HeartItem : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _collectSound;

    public event UnityAction HeartCollected;

    private void OnEnable()
    {
        HeartCollected += OnHeartCollected;
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

        if (collision.gameObject.TryGetComponent<King>(out King king))
        {
            king.ApplyHeal(1);
            HeartCollected?.Invoke();
        }
    }

    private void EraseHeart()
    {
        HeartCollected -= OnHeartCollected;
        Destroy(gameObject);
    }

    private void OnHeartCollected()
    {
        _animator.SetTrigger("Collected");
        _collectSound.Play();
    }
}
