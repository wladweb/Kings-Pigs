using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Blow : MonoBehaviour
{
    [SerializeField] private int _damage = 300;

    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<King>(out King king))
        {
            king.ApplyDamage(_damage);
        }
    }

    private void RemoveBlow()
    {
        gameObject.SetActive(false);
    }
}
