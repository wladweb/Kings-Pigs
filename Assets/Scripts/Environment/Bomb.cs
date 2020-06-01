using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject _blow;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_blow != null)
        {
            _blow.transform.position = transform.position;
            _blow.SetActive(true);
        }
        
        gameObject.SetActive(false);
    }

    public void SetBlow(Blows blows)
    {
        _blow = blows.GetBlow();
    }

    public void Armed()
    {
        GetComponent<Animator>().SetTrigger("BombOn");
    }
}
