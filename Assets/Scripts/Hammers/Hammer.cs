using UnityEngine;

[CreateAssetMenu(fileName ="Hammer", menuName = "Hammer", order = 1)]
public class Hammer : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _hammerName;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _length;
    [SerializeField] private int _price;
    [SerializeField] private Color _color;

    public Sprite Icon => _icon;
    public string HammerName => _hammerName;
    public float Speed => _speed;
    public int Damage => _damage;
    public float Length => _length;
    public int Price => _price;
    public Color Color => _color;
}
