using UnityEngine;

[CreateAssetMenu(fileName ="Hammer", menuName = "Hammer", order = 1)]
public class Hammer : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _hammerName;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _price;

    public Sprite Icon => _icon;
    public string HammerName => _hammerName;
    public float Speed => _speed;
    public int Damage => _damage;
    public int Price => _price;
}
