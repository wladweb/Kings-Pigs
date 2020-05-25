using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class King : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] Hammer _hammer;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _isLanded = true;
    private bool _isFacingRight = true;
    private float _currentDirection = 1;
    private float _timeAfterLastAttack;
    private List<Hammer> _hammers = new List<Hammer>();

    public int Diamonds { get; private set; } = 100;

    public event UnityAction<int> DiamondsCountChanged;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0)
            _currentDirection = direction;

        if (_currentDirection < 0 && _isFacingRight)
            Flip();
        else if (_currentDirection > 0 && !_isFacingRight)
            Flip();

        _rigidBody.velocity = new Vector2(direction * _speed, _rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && _isLanded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetFloat("VerticalSpeed", _rigidBody.velocity.y);
    }

    private void Attack()
    {
        _timeAfterLastAttack += Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            if (_timeAfterLastAttack > (1 / _hammer.Speed))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right * _currentDirection, _hammer.Length);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.TryGetComponent<Pig>(out Pig pig))
                        pig.ApplyDamage(_hammer.Damage);
                }

                _animator.SetTrigger("Fire");
                _timeAfterLastAttack = 0;
            }
        }
    }

    public void BuyingHammer(Hammer hammer)
    {
        _hammers.Add(hammer);
        _hammer = hammer;

        Diamonds -= hammer.Price;
        DiamondsCountChanged?.Invoke(Diamonds);
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
