using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorIn : MonoBehaviour
{
    [SerializeField] private GameObject _king;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.Play("Opening");
    }

    private void PlaceKing()
    {
        _king.transform.position = transform.GetChild(0).transform.position;
        _king.SetActive(true);
    }
}
