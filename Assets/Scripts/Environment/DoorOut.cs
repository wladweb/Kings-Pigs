using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class DoorOut : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<King>(out King king))
        {
            _animator.Play("Closing");
            king.LockControls = true;
            king.GetComponent<Animator>().Play("DoorIn");
        }
    }
}
