using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PigWarrior : Pig
{
    [SerializeField] private float _leftConstraint;
    [SerializeField] private float _rightConstraint;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _hitsPerSecond;

    private Transform _king;
    private float _startPositionX;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private int _currentDirection = -1;
    private bool _isFacingRight;
    private float _timeAfterLastHit;
    private bool _move = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startPositionX = transform.position.x;
        _king = FindObjectOfType<TheGame>().GetKingTransform();
    }

    private enum WarriorState 
    {
        Idle,
        Attack
    }

    private WarriorState _currentState = WarriorState.Idle;

    private void Update()
    {
        if (IsActive)
        {
            if (_currentState == WarriorState.Idle)
            {
                GetPatrolDirection();
            }
            else if (_currentState == WarriorState.Attack)
            {
                GetPlayerDirection();
            }

            if (_move)
                Move(_currentDirection);
        }
    }

    private void GetPlayerDirection()
    {
        if (_king.position.x <= transform.position.x)
            _currentDirection = -1;
        else if (_king.position.x >= transform.position.x)
            _currentDirection = 1;
    }

    private void GetPatrolDirection()
    {
        float leftConstraintX = _startPositionX - _leftConstraint;
        float rightConstraintX = _startPositionX + _rightConstraint;

        if (transform.position.x < leftConstraintX)
            _currentDirection = 1;
        else if (transform.position.x > rightConstraintX)
            _currentDirection = -1;
    }

    private void Move(int direction)
    {
        if (_currentDirection < 0 && _isFacingRight)
            Flip();
        else if (_currentDirection > 0 && !_isFacingRight)
            Flip();

        _rigidBody.velocity = new Vector2(direction * _speed, _rigidBody.velocity.y);

        _animator.SetFloat("Speed", _rigidBody.velocity.sqrMagnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<King>(out King king))
        {
            _currentState = WarriorState.Attack;
            _move = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<King>(out King king))
        {
            _timeAfterLastHit += Time.deltaTime;

            if (_timeAfterLastHit >= (1f / _hitsPerSecond))
            {
                king.ApplyDamage(_damage);
                _timeAfterLastHit = 0;
            }

            _animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<King>(out King king))
        {
            _animator.SetBool("Attack", false);
            _move = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PigVulnerability>(out PigVulnerability pig))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider); ; ;
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
