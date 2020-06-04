using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private float _shootForce;

    private CannonBalls _balls;
    private Blows _blows;

    private void Awake()
    {
        _balls = FindObjectOfType<CannonBalls>();
        _blows = FindObjectOfType<Blows>();
    }

    private void Shoot()
    {
        GameObject ball = _balls.GetItem();
        
        if (ball != null)
        {
            float shootDirectionX = _shootForce * Random.Range(.8f, 1.5f);

            ball.transform.position = _shootPoint.transform.position;
            ball.SetActive(true);
            ball.GetComponent<CannonBall>().SetBlowsPool(_blows);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootDirectionX, 0), ForceMode2D.Impulse);
        }
    }

    
}
