using UnityEngine;

public class Pig : MonoBehaviour
{
    public void ApplyDamage(int damage)
    {
        Debug.Log($"Take Damage {damage}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<King>(out King king))
        {
            king.ApplyDamage(3 00);
        }
    }
}
