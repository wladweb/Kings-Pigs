using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondsBar : MonoBehaviour
{
    [SerializeField] private King _king;
    [SerializeField] private Sprite[] _numberSprites;

    private List<Transform> _digits = new List<Transform>();

    private void Awake()
    {
        for (int i = 0, l = transform.childCount; i < l; i++)
        {
            _digits.Add(transform.GetChild(i));
        }
    }

    private void OnEnable()
    {
        _king.DiamondsCountChanged += OnDiamondsCountChange;
    }

    private void OnDisable()
    {
        _king.DiamondsCountChanged -= OnDiamondsCountChange;
    }

    private void OnDiamondsCountChange(int diamondsCount)
    {
        char[] digits = diamondsCount.ToString().ToCharArray();
        List<char> digitsList = new List<char>(digits);

        for (int i = 1, l = _digits.Count - digitsList.Count; i <= l; i++)
            digitsList.Insert(0, '0');

        for (int i = 0, l = _digits.Count; i < l; i++)
        {
            int index = (int) Char.GetNumericValue(digitsList[i]);
            _digits[i].GetComponent<Image>().sprite = _numberSprites[index];
        }
    }
}
