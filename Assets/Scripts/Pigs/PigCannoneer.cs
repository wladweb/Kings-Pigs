using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PigCannoneer : Pig
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] float _secondsBetweenShoots;

    private float _timeFromLastShoot;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsActive)
        {
            _timeFromLastShoot += Time.deltaTime;

            if (_timeFromLastShoot >= _secondsBetweenShoots)
            {
                Shoot();
                _timeFromLastShoot = 0;
            }
        }
    }

    private void Shoot()
    {
        _animator.SetTrigger("Shoot");
    }

    private void CannonShoot()
    {
        _cannon.GetComponent<Animator>().SetTrigger("Shoot");
    }
}
