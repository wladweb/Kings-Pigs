using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class King : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] Hammer _hammer;
    [SerializeField] HealthBar _bar;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _isLanded = true;
    private bool _isFacingRight = true;
    private float _currentDirection = 1;
    private float _timeAfterLastAttack;
    private List<Hammer> _hammers = new List<Hammer>();
    private int _incomingHeartDamage;
    private int _hammerIndex;
    private AudioSource _hitSound;


    public bool LockControls { get; set; }
    public int HammerIndex 
    {
        get 
        {
            return _hammerIndex;
        }
        set 
        {
            if (value > _hammers.Count - 1)
                _hammerIndex = 0;
            else if (value < 0)
                _hammerIndex = _hammers.Count - 1;
            else
                _hammerIndex = value;
        }
    }
    public int Diamonds { get; private set; } = 237;

    public event UnityAction<int> DiamondsCountChanged;
    public event UnityAction<int> HealthChanged;
    public event UnityAction<Hammer> HammerChanged;
    public event UnityAction MoveThroughExitDoor;
    public event UnityAction KingDeadAnimationStop;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _hitSound = GetComponent<AudioSource>();
        DiamondsCountChanged?.Invoke(Diamonds);

        _hammers.Add(_hammer);
        HammerChanged?.Invoke(_hammer);
    }

    private void OnEnable()
    {
        _bar.KingDied += OnKingDied;
    }

    private void OnDisable()
    {
        _bar.KingDied -= OnKingDied;
    }

    private void Update()
    {
        if (!LockControls)
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
                _hitSound.Play();

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.TryGetComponent<PigVulnerability>(out PigVulnerability pig))
                        pig.ApplyDamage(_hammer.Damage);
                    else if (hit.transform.TryGetComponent<BoxItem>(out BoxItem boxItem))
                        boxItem.ApplyDamage(transform.position.x);
                }

                _animator.SetTrigger("Fire");
                _timeAfterLastAttack = 0;
            }
        }
    }

    public void ApplyDamage(int percentOfOneHeart)
    {
        _animator.SetTrigger("Hit");

        _incomingHeartDamage += percentOfOneHeart;
        int heartsCountToDestroy = _incomingHeartDamage / 100;

        if (heartsCountToDestroy >= 1)
        {
            _incomingHeartDamage %= 100;
            HealthChanged?.Invoke(-heartsCountToDestroy);
        }
    }

    public void ApplyHeal(int heartsCount)
    {
        HealthChanged?.Invoke(heartsCount);
    }

    public void BuyingHammer(Hammer hammer)
    {
        _hammers.Add(hammer);
        Diamonds -= hammer.Price;
        DiamondsCountChanged?.Invoke(Diamonds);
    }

    public void CollectDiamond(int diamondsCount)
    {
        Diamonds += diamondsCount;
        DiamondsCountChanged?.Invoke(Diamonds);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Platform>(out Platform platform) ||
            collision.transform.TryGetComponent<RigidBox>(out RigidBox box))
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

    private void OnKingDied()
    {
        _animator.SetTrigger("Dead");
        LockControls = true;
    }

    public void GetPreviousHammer() 
    {
        HammerIndex = _hammers.IndexOf(_hammer);
        HammerIndex--;
        _hammer = _hammers[HammerIndex];
        HammerChanged?.Invoke(_hammer);
    }

    public void GetNextHamer()
    {
        HammerIndex = _hammers.IndexOf(_hammer);
        HammerIndex++;
        _hammer = _hammers[HammerIndex];
        HammerChanged?.Invoke(_hammer);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void MoveOut()
    {
        gameObject.SetActive(false);
        MoveThroughExitDoor?.Invoke();
    }

    private void DeadAnimationStop()
    {
        KingDeadAnimationStop?.Invoke();
    }
}
