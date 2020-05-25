using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonTextMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform[] _elementsToMove;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;

    private Vector2[] _startPositions;
    private Button _button;

    private void Start()
    {
        _startPositions = new Vector2[_elementsToMove.Length];
        _button = GetComponent<Button>();

        for (int i = 0, l = _elementsToMove.Length; i < l; i++)
        {
            _startPositions[i] = _elementsToMove[i].localPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable == true)
        {
            for (int i = 0, l = _elementsToMove.Length; i < l; i++)
            {
                Vector2 startPosition = _startPositions[i];
                _elementsToMove[i].localPosition = new Vector2(startPosition.x - _offsetX, startPosition.y - _offsetY);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable == true)
        {
            for (int i = 0, l = _elementsToMove.Length; i < l; i++)
            {
                _elementsToMove[i].localPosition = _startPositions[i];
            }
        }
    }
}
