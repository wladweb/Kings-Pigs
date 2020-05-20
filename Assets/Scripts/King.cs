using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class King : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
}
