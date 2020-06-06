using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class DoorOut : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _sound;

    public event UnityAction KingLeftRoom;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<King>(out King king))
        {
            _animator.Play("Closing");
            king.LockControls = true;
            king.GetComponent<Animator>().Play("DoorIn");
            _sound.Play();
            KingLeftRoom?.Invoke();
        }
    }
}
