using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class King : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _isLanded = true;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        if (direction < 0 && _isFacingRight)
            Flip();
        else if (direction > 0 && !_isFacingRight)
            Flip();

        _rigidBody.velocity = new Vector2(direction * _speed, _rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && _isLanded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Fire");
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetFloat("VerticalSpeed", _rigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Platform>(out Platform platform))
        {
            _isLanded = true;
            _animator.SetTrigger("Landing");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Platform>(out Platform platform))
        {
            _isLanded = false;
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
