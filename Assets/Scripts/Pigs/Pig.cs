using UnityEngine;
using UnityEngine.Events;

public class Pig : MonoBehaviour
{
    [SerializeField] int _health;
    [SerializeField] ParticleSystem _blood;
    

    private Animator _animator;

    public event UnityAction Died;
    public event UnityAction TookDamage;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Died += OnPigDied;
        TookDamage += DamageTakeAnimation;
    }

    private void OnDisable()
    {
        Died -= OnPigDied;
        TookDamage -= DamageTakeAnimation;
    }

    public void ApplyDamage(int damage)
    {
        TookDamage?.Invoke();

        _health -= damage;

        if (_health <= 0)
            Died?.Invoke();
    }

    private void DamageTakeAnimation()
    {
        _animator.SetTrigger("Hit");
        _blood.Play();
    }

    private void OnPigDied()
    {
        _animator.SetBool("Dead", true);
    }

    private void DestroyPig()
    {
        Destroy(gameObject);
    }
}
