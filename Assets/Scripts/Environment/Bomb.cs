using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Blows _blows;

    private void OnEnable()
    {
        _blows = FindObjectOfType<Blows>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject blow = _blows.GetBlow();

        if (blow != null)
        {
            blow.transform.position = transform.position;
            blow.SetActive(true);
        }
        
        gameObject.SetActive(false);
    }

    public void Armed()
    {
        GetComponent<Animator>().SetTrigger("BombOn");
    }
}
