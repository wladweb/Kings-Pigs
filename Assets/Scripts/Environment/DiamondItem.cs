using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DiamondItem : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _collectSound;

    public event UnityAction DiamondCollected;

    private void OnEnable()
    {
        DiamondCollected += OnDiamondCollect;
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
            king.CollectDiamond(1);
            DiamondCollected?.Invoke();
        }
    }

    private void OnDiamondCollect()
    {
        _animator.SetTrigger("Collected");
        _collectSound.Play();
    }

    private void EraseDiamond()
    {
        DiamondCollected -= OnDiamondCollect;
        Destroy(gameObject);
    }
}
