using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Blows _blows;

    public void SetBlowsPool(Blows blows)
    {
        _blows = blows;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        GameObject blow = _blows.GetItem();

        if (blow != null)
        {
            blow.transform.position = transform.position;
            blow.SetActive(true);
        }
    }
}
