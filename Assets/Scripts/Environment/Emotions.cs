using UnityEngine;

public class Emotions : MonoBehaviour
{
    [SerializeField] private Transform _bubblePoint;
    [SerializeField] private float _secondsBetweenBubles;

    private float _secondsAfterLastBubble;
    private Bubbles _bubbles;
    private GameObject _bubble;
    private float _delay;

    private void Start()
    {
        _bubbles = FindObjectOfType<Bubbles>();
        _delay = Random.Range(.3f, 1.3f);
    }

    private void Update()
    {
        _secondsAfterLastBubble += Time.deltaTime;

        if (_secondsAfterLastBubble >= (_secondsBetweenBubles + _delay))
        {
            _bubble =_bubbles.GetItem();

            if (_bubble != null)
            {
                _bubble.gameObject.SetActive(true);
                _secondsAfterLastBubble = 0;
            }
        }

        if (_bubble != null)
            _bubble.transform.position = _bubblePoint.position;
    }
}
